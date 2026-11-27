using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SpeechApp.Forms;

namespace SpeechApp
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "--generate-screenshots")
            {
                GenerateScreenshots();
                return;
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }

        private static void GenerateScreenshots()
        {
            string screenshotsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "screenshots");
            Directory.CreateDirectory(screenshotsDir);

            ApplicationConfiguration.Initialize();

            // 1. Ekran Görüntüsü: Metinden Sese (Text-to-Speech) Paneli & Vurgulu Okuma
            using (MainForm form1 = new MainForm())
            {
                form1.Show();
                form1.Size = new Size(1060, 674);

                var tabMain = form1.Controls.Find("tabMain", true)[0] as TabControl;
                if (tabMain != null) tabMain.SelectedIndex = 0;

                var lblTtsStatus = form1.Controls.Find("lblTtsStatus", true)[0] as Label;
                var btnSpeak = form1.Controls.Find("btnTtsSpeak", true)[0] as Button;
                var btnPause = form1.Controls.Find("btnTtsPause", true)[0] as Button;
                var btnStop = form1.Controls.Find("btnTtsStop", true)[0] as Button;
                var rtbTts = form1.Controls.Find("rtbTtsInput", true)[0] as RichTextBox;

                if (lblTtsStatus != null)
                {
                    lblTtsStatus.Text = "● Seslendiriliyor...";
                    lblTtsStatus.BackColor = Color.FromArgb(37, 99, 235);
                }

                if (btnSpeak != null) btnSpeak.Enabled = false;
                if (btnPause != null) btnPause.Enabled = true;
                if (btnStop != null) btnStop.Enabled = true;

                if (rtbTts != null)
                {
                    rtbTts.Text = "Merhaba! Bu uygulama, Nesne Tabanlı Programlama (YMH 219) dersi kapsamında geliştirilmiş Speech-to-Text ve Text-to-Speech masaüstü yazılımıdır.\n\nSeslendirilecek metni buraya yazabilir veya düzenleyebilirsiniz. Sağ üstteki panelden ses hızını ve ses seviyesini dilediğiniz gibi ayarlayabilirsiniz.";
                    // Vurgulu kelime simülasyonu
                    int start = rtbTts.Text.IndexOf("Speech-to-Text");
                    if (start >= 0)
                    {
                        rtbTts.Select(start, "Speech-to-Text ve Text-to-Speech".Length);
                        rtbTts.SelectionBackColor = Color.FromArgb(254, 240, 138); // pastel yellow
                    }
                }

                Application.DoEvents();
                Thread.Sleep(200);

                using (Bitmap bmp = new Bitmap(form1.Width, form1.Height))
                {
                    form1.DrawToBitmap(bmp, new Rectangle(0, 0, form1.Width, form1.Height));
                    bmp.Save(Path.Combine(screenshotsDir, "tts_panel.png"), ImageFormat.Png);
                }

                form1.Close();
            }

            // 2. Ekran Görüntüsü: Sesten Metne (Speech-to-Text) Paneli & Zaman Damgalı Geçmiş
            using (MainForm form2 = new MainForm())
            {
                form2.Show();
                form2.Size = new Size(1060, 674);

                var tabMain = form2.Controls.Find("tabMain", true)[0] as TabControl;
                if (tabMain != null) tabMain.SelectedIndex = 1; // STT Sekmesi

                var lblSttStatus = form2.Controls.Find("lblSttStatus", true)[0] as Label;
                var btnStart = form2.Controls.Find("btnSttStart", true)[0] as Button;
                var btnStop = form2.Controls.Find("btnSttStop", true)[0] as Button;
                var prgAudio = form2.Controls.Find("prgAudioLevel", true)[0] as ProgressBar;
                var rtbTranscript = form2.Controls.Find("rtbSttTranscript", true)[0] as RichTextBox;
                var dgvLogs = form2.Controls.Find("dgvSpeechLogs", true)[0] as DataGridView;
                var grpLogs = form2.Controls.Find("grpSpeechLogs", true)[0] as GroupBox;

                if (lblSttStatus != null)
                {
                    lblSttStatus.Text = "● Mikrofon Dinleniyor";
                    lblSttStatus.BackColor = Color.FromArgb(22, 163, 74);
                }

                if (btnStart != null) btnStart.Enabled = false;
                if (btnStop != null) btnStop.Enabled = true;
                if (prgAudio != null) prgAudio.Value = 68;

                if (rtbTranscript != null)
                {
                    rtbTranscript.AppendText($"[{DateTime.Now.AddSeconds(-25):HH:mm:ss}] Merhaba dünya, ses tanıma testi başlatıldı.\n");
                    rtbTranscript.AppendText($"[{DateTime.Now.AddSeconds(-18):HH:mm:ss}] Nesne Tabanlı Programlama YMH 219 laboratuvar ödevi.\n");
                    rtbTranscript.AppendText($"[{DateTime.Now.AddSeconds(-10):HH:mm:ss}] Öğrenci: İsmail Özdemir, Numara: 250541109.\n");
                    rtbTranscript.AppendText($"[{DateTime.Now.AddSeconds(-2):HH:mm:ss}] Konuşma algılama ve transkript başarıyla oluşturuldu.\n");
                }

                if (dgvLogs != null)
                {
                    dgvLogs.Rows.Add(4, DateTime.Now.AddSeconds(-2).ToString("HH:mm:ss.fff"), "Mikrofon", "%96.4", "Konuşma algılama ve transkript başarıyla oluşturuldu.");
                    dgvLogs.Rows.Add(3, DateTime.Now.AddSeconds(-10).ToString("HH:mm:ss.fff"), "Mikrofon", "%94.2", "Öğrenci: İsmail Özdemir, Numara: 250541109.");
                    dgvLogs.Rows.Add(2, DateTime.Now.AddSeconds(-18).ToString("HH:mm:ss.fff"), "Mikrofon", "%91.8", "Nesne Tabanlı Programlama YMH 219 laboratuvar ödevi.");
                    dgvLogs.Rows.Add(1, DateTime.Now.AddSeconds(-25).ToString("HH:mm:ss.fff"), "Mikrofon", "%89.5", "Merhaba dünya, ses tanıma testi başlatıldı.");
                }

                if (grpLogs != null) grpLogs.Text = "🕒 Zaman Damgalı Algılanan Konuşma Geçmişi (4)";

                Application.DoEvents();
                Thread.Sleep(200);

                using (Bitmap bmp = new Bitmap(form2.Width, form2.Height))
                {
                    form2.DrawToBitmap(bmp, new Rectangle(0, 0, form2.Width, form2.Height));
                    bmp.Save(Path.Combine(screenshotsDir, "stt_panel.png"), ImageFormat.Png);
                }

                form2.Close();
            }

            Console.WriteLine("SpeechApp ekran görüntüleri başarıyla üretildi: " + screenshotsDir);
        }
    }
}
