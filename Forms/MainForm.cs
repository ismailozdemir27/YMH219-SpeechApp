using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpeechApp.Enums;
using SpeechApp.Models;
using SpeechApp.Services;

namespace SpeechApp.Forms
{
    public partial class MainForm : Form
    {
        private readonly TextToSpeechService _ttsService;
        private readonly SpeechToTextService _sttService;
        private readonly FileExportService _exportService;

        private List<VoiceModel> _availableVoices = new List<VoiceModel>();
        private int _lastHighlightPos = -1;
        private int _lastHighlightLen = 0;
        private float _currentFontSize = 12.0f;
        private readonly Random _rand = new Random();

        public MainForm()
        {
            InitializeComponent();

            _ttsService = new TextToSpeechService();
            _sttService = new SpeechToTextService();
            _exportService = new FileExportService();

            InitializeWaveformBars();
            InitializeGrid();
            InitializeSamplePhrases();
            BindTtsEvents();
            BindSttEvents();

            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            LoadInstalledVoices();
            UpdateRecognizerDisplay();
            UpdateTtsUIState(SynthesisStatus.Idle);
            UpdateSttUIState(_sttService.Status, "Hazır");
            UpdateTextStatistics();

            tmrWaveform.Interval = 90;
            tmrWaveform.Tick += TmrWaveform_Tick;
            tmrWaveform.Start();
        }

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            tmrWaveform.Stop();
            _ttsService.Stop();
            _sttService.StopListening();
            _ttsService.Dispose();
            _sttService.Dispose();
        }

        #region --- GÖRSEL DALGA VE GRİD İNİT ---

        private void InitializeWaveformBars()
        {
            lblBars = new Label[5];
            int barWidth = 8;
            int gap = 5;

            for (int i = 0; i < 5; i++)
            {
                lblBars[i] = new Label
                {
                    Width = barWidth,
                    Height = 6,
                    BackColor = Color.FromArgb(56, 189, 248), // sky blue
                    Location = new Point(i * (barWidth + gap) + 10, 20),
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Left
                };
                pnlWaveform.Controls.Add(lblBars[i]);
            }
        }

        private void TmrWaveform_Tick(object? sender, EventArgs e)
        {
            bool active = _ttsService.Status == SynthesisStatus.Speaking || _sttService.Status == RecognitionStatus.Listening;

            for (int i = 0; i < lblBars.Length; i++)
            {
                int targetHeight = active ? _rand.Next(6, 26) : 4;
                lblBars[i].Height = targetHeight;
                lblBars[i].Top = pnlWaveform.Height - targetHeight - 2;
                lblBars[i].BackColor = active ? Color.FromArgb(74, 222, 128) : Color.FromArgb(100, 116, 139);
            }
        }

        private void InitializeGrid()
        {
            dgvSpeechLogs.Columns.Clear();

            dgvSpeechLogs.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                HeaderText = "#",
                Width = 45
            });

            dgvSpeechLogs.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTime",
                HeaderText = "Zaman Damgası",
                Width = 135
            });

            dgvSpeechLogs.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSource",
                HeaderText = "Kaynak",
                Width = 110
            });

            dgvSpeechLogs.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colConfidence",
                HeaderText = "Güven",
                Width = 85
            });

            dgvSpeechLogs.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colText",
                HeaderText = "Algılanan Cümle",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        private void InitializeSamplePhrases()
        {
            cmbSamplePhrases.Items.Clear();
            cmbSamplePhrases.Items.Add("Merhaba dünya, Türkçe konuşma tanıma testi.");
            cmbSamplePhrases.Items.Add("Nesne Tabanlı Programlama (YMH 219) laboratuvar ödevi.");
            cmbSamplePhrases.Items.Add("İsmail Özdemir, öğrenci numarası: 250541109.");
            cmbSamplePhrases.Items.Add("Windows Forms ve C# ile modern arayüz tasarımı.");
            cmbSamplePhrases.Items.Add("Text-to-Speech ve Speech-to-Text çift yönlü dönüşüm çalışıyor.");
            cmbSamplePhrases.SelectedIndex = 0;
        }

        private void UpdateRecognizerDisplay()
        {
            lblRecognizerInfo.Text = $"Motor: {_sttService.RecognizerName}";
            lblRecognizerInfo.ForeColor = _sttService.IsRecognizerAvailable ? Color.FromArgb(74, 222, 128) : Color.FromArgb(248, 113, 113);
        }

        #endregion

        #region --- TEXT-TO-SPEECH (TTS) İŞLEMLERİ ---

        private void LoadInstalledVoices()
        {
            _availableVoices = _ttsService.GetAvailableVoices();
            cmbVoices.Items.Clear();

            if (_availableVoices.Count == 0)
            {
                cmbVoices.Items.Add("Sistemde Ses Bulunamadı");
                cmbVoices.SelectedIndex = 0;
                cmbVoices.Enabled = false;
                btnTtsSpeak.Enabled = false;
                return;
            }

            foreach (var voice in _availableVoices)
            {
                cmbVoices.Items.Add(voice);
            }

            cmbVoices.SelectedIndex = 0;
            _ttsService.SelectVoice(_availableVoices[0]);
        }

        private void BindTtsEvents()
        {
            _ttsService.OnStatusChanged += Tts_OnStatusChanged;
            _ttsService.OnSpeakProgress += Tts_OnSpeakProgress;
            _ttsService.OnSpeakCompleted += Tts_OnSpeakCompleted;
            _ttsService.OnError += Tts_OnError;

            // Metin değiştiğinde istatistikleri güncelle
            rtbTtsInput.TextChanged += (s, e) => UpdateTextStatistics();

            // Sürükle-bırak (Drag & Drop) ile .txt açma
            rtbTtsInput.DragEnter += (s, e) =>
            {
                if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    e.Effect = DragDropEffects.Copy;
                }
            };
            rtbTtsInput.DragDrop += (s, e) =>
            {
                if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[]? files = e.Data.GetData(DataFormats.FileDrop) as string[];
                    if (files != null && files.Length > 0 && File.Exists(files[0]))
                    {
                        LoadTextFromFile(files[0]);
                    }
                }
            };

            // Font boyutu ayarı (A+ / A-)
            btnZoomIn.Click += (s, e) =>
            {
                if (_currentFontSize < 24.0f)
                {
                    _currentFontSize += 2.0f;
                    rtbTtsInput.Font = new Font(rtbTtsInput.Font.FontFamily, _currentFontSize);
                    rtbSttTranscript.Font = new Font(rtbSttTranscript.Font.FontFamily, _currentFontSize);
                }
            };

            btnZoomOut.Click += (s, e) =>
            {
                if (_currentFontSize > 9.0f)
                {
                    _currentFontSize -= 2.0f;
                    rtbTtsInput.Font = new Font(rtbTtsInput.Font.FontFamily, _currentFontSize);
                    rtbSttTranscript.Font = new Font(rtbSttTranscript.Font.FontFamily, _currentFontSize);
                }
            };

            // Ses profili değişimi
            cmbVoices.SelectedIndexChanged += (s, e) =>
            {
                if (cmbVoices.SelectedItem is VoiceModel selected)
                {
                    _ttsService.SelectVoice(selected);
                    lblFooterStatus.Text = $"Aktif Ses: {selected.DisplayName}";
                    UpdateTextStatistics();
                }
            };

            // Hız ayarı
            tbRate.ValueChanged += (s, e) =>
            {
                _ttsService.Rate = tbRate.Value;
                lblRateValue.Text = tbRate.Value > 0 ? $"+{tbRate.Value}" : tbRate.Value.ToString();
                UpdateTextStatistics();
            };

            // Ses düzeyi ayarı
            tbVolume.ValueChanged += (s, e) =>
            {
                _ttsService.Volume = tbVolume.Value;
                lblVolumeValue.Text = $"%{tbVolume.Value}";
            };

            // Oynat
            btnTtsSpeak.Click += async (s, e) =>
            {
                string text = rtbTtsInput.Text.Trim();
                if (string.IsNullOrWhiteSpace(text))
                {
                    MessageBox.Show("Lütfen seslendirilecek bir metin giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rtbTtsInput.Focus();
                    return;
                }

                ClearTextHighlights();
                await _ttsService.SpeakAsync(text);
            };

            btnTtsPause.Click += (s, e) => _ttsService.Pause();
            btnTtsResume.Click += (s, e) => _ttsService.Resume();
            btnTtsStop.Click += (s, e) =>
            {
                _ttsService.Stop();
                ClearTextHighlights();
            };

            btnTtsClear.Click += (s, e) =>
            {
                if (rtbTtsInput.TextLength > 0)
                {
                    if (MessageBox.Show("Metin alanını temizlemek istediğinize emin misiniz?", "Temizle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _ttsService.Stop();
                        rtbTtsInput.Clear();
                        ClearTextHighlights();
                        UpdateTextStatistics();
                    }
                }
            };

            // Dosyadan Yükle (.txt)
            btnTtsOpenFile.Click += (s, e) =>
            {
                using OpenFileDialog ofd = new OpenFileDialog
                {
                    Filter = "Metin Belgeleri (*.txt)|*.txt|Tüm Dosyalar (*.*)|*.*",
                    Title = "Seslendirilecek Metin Dosyasını Seçin"
                };

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    LoadTextFromFile(ofd.FileName);
                }
            };

            // Metni Kaydet (.txt)
            btnTtsSaveText.Click += (s, e) =>
            {
                string text = rtbTtsInput.Text.Trim();
                if (string.IsNullOrWhiteSpace(text))
                {
                    MessageBox.Show("Kaydedilecek metin bulunmuyor.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Metin Belgesi (*.txt)|*.txt",
                    FileName = $"TTS_Metin_{DateTime.Now:yyyyMMdd_HHmmss}.txt",
                    Title = "Metni TXT Olarak Kaydet"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (_exportService.SaveTextToFile(sfd.FileName, text))
                    {
                        MessageBox.Show($"Metin başarıyla kaydedildi:\n{sfd.FileName}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Dosya kaydedilirken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };

            // Sesi Kaydet (.wav)
            btnTtsSaveWav.Click += async (s, e) =>
            {
                string text = rtbTtsInput.Text.Trim();
                if (string.IsNullOrWhiteSpace(text))
                {
                    MessageBox.Show("Ses dosyasına dönüştürülecek metin bulunmuyor.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "WAV Ses Dosyası (*.wav)|*.wav",
                    FileName = $"TTS_Ses_{DateTime.Now:yyyyMMdd_HHmmss}.wav",
                    Title = "Metni WAV Ses Dosyası Olarak Kaydet"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    btnTtsSaveWav.Enabled = false;
                    lblFooterStatus.Text = "WAV ses dosyası üretiliyor...";
                    bool ok = await _ttsService.ExportToWavAsync(text, sfd.FileName);
                    btnTtsSaveWav.Enabled = true;

                    if (ok)
                    {
                        MessageBox.Show($"WAV ses dosyası başarıyla üretildi:\n{sfd.FileName}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblFooterStatus.Text = "WAV ses dosyası kaydedildi.";
                    }
                    else
                    {
                        MessageBox.Show("WAV dosyası oluşturulurken hata meydana geldi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };
        }

        private void LoadTextFromFile(string filePath)
        {
            try
            {
                string content = File.ReadAllText(filePath, System.Text.Encoding.UTF8);
                rtbTtsInput.Text = content;
                UpdateTextStatistics();
                lblFooterStatus.Text = $"Dosya yüklendi: {Path.GetFileName(filePath)}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Dosya okunamadı: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTextStatistics()
        {
            string text = rtbTtsInput.Text;
            int charCount = text.Length;

            // Kelime sayısı hesabı
            MatchCollection wordMatches = Regex.Matches(text, @"[\p{L}\p{N}]+");
            int wordCount = wordMatches.Count;

            // Cümle sayısı hesabı
            MatchCollection sentenceMatches = Regex.Matches(text, @"[.!?]+");
            int sentenceCount = sentenceMatches.Count > 0 ? sentenceMatches.Count : (wordCount > 0 ? 1 : 0);

            // Ortalama okuma hızı: normal hızda dakikada ~130 kelime
            double speedFactor = tbRate.Value < 0 ? (1.0 + tbRate.Value * 0.05) : (1.0 + tbRate.Value * 0.1);
            int estSeconds = wordCount > 0 ? Math.Max(1, (int)(wordCount / (130.0 / 60.0 * speedFactor))) : 0;

            lblTtsStats.Text = $"📊 Karakter: {charCount:N0} | Kelime: {wordCount:N0} | Cümle: {sentenceCount:N0} | ⏱️ Tahmini Okuma Süresi: ~{estSeconds} sn";
        }

        private void Tts_OnStatusChanged(SynthesisStatus status)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Tts_OnStatusChanged(status)));
                return;
            }

            UpdateTtsUIState(status);
        }

        private void UpdateTtsUIState(SynthesisStatus status)
        {
            switch (status)
            {
                case SynthesisStatus.Idle:
                    lblTtsStatus.Text = "● Hazır";
                    lblTtsStatus.BackColor = Color.FromArgb(22, 163, 74);
                    btnTtsSpeak.Enabled = _availableVoices.Count > 0;
                    btnTtsPause.Enabled = false;
                    btnTtsResume.Enabled = false;
                    btnTtsStop.Enabled = false;
                    btnTtsClear.Enabled = true;
                    cmbVoices.Enabled = true;
                    lblFooterStatus.Text = "Metinden Sese (TTS) Motoru: Hazır";
                    break;

                case SynthesisStatus.Speaking:
                    lblTtsStatus.Text = "● Seslendiriliyor...";
                    lblTtsStatus.BackColor = Color.FromArgb(37, 99, 235);
                    btnTtsSpeak.Enabled = false;
                    btnTtsPause.Enabled = true;
                    btnTtsResume.Enabled = false;
                    btnTtsStop.Enabled = true;
                    btnTtsClear.Enabled = false;
                    cmbVoices.Enabled = false;
                    lblFooterStatus.Text = "Metin seslendiriliyor...";
                    break;

                case SynthesisStatus.Paused:
                    lblTtsStatus.Text = "■ Duraklatıldı";
                    lblTtsStatus.BackColor = Color.FromArgb(217, 119, 6);
                    btnTtsSpeak.Enabled = false;
                    btnTtsPause.Enabled = false;
                    btnTtsResume.Enabled = true;
                    btnTtsStop.Enabled = true;
                    btnTtsClear.Enabled = false;
                    lblFooterStatus.Text = "Seslendirme duraklatıldı. Devam etmek için 'Devam'a basınız.";
                    break;
            }
        }

        private void Tts_OnSpeakProgress(int characterPosition, int characterCount, string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Tts_OnSpeakProgress(characterPosition, characterCount, text)));
                return;
            }

            HighlightSpokenWord(characterPosition, characterCount);
        }

        private void HighlightSpokenWord(int pos, int count)
        {
            try
            {
                if (_lastHighlightPos >= 0 && _lastHighlightPos + _lastHighlightLen <= rtbTtsInput.TextLength)
                {
                    rtbTtsInput.Select(_lastHighlightPos, _lastHighlightLen);
                    rtbTtsInput.SelectionBackColor = Color.White;
                }

                if (pos >= 0 && pos + count <= rtbTtsInput.TextLength)
                {
                    rtbTtsInput.Select(pos, count);
                    rtbTtsInput.SelectionBackColor = Color.FromArgb(254, 240, 138); // pastel yellow
                    rtbTtsInput.SelectionColor = Color.FromArgb(15, 23, 42);
                    _lastHighlightPos = pos;
                    _lastHighlightLen = count;
                }
            }
            catch { }
        }

        private void ClearTextHighlights()
        {
            try
            {
                int currentSelection = rtbTtsInput.SelectionStart;
                rtbTtsInput.SelectAll();
                rtbTtsInput.SelectionBackColor = Color.White;
                rtbTtsInput.SelectionColor = Color.FromArgb(15, 23, 42);
                rtbTtsInput.Select(currentSelection, 0);
                _lastHighlightPos = -1;
                _lastHighlightLen = 0;
            }
            catch { }
        }

        private void Tts_OnSpeakCompleted()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(Tts_OnSpeakCompleted));
                return;
            }

            ClearTextHighlights();
            UpdateTtsUIState(SynthesisStatus.Idle);
            lblFooterStatus.Text = "Seslendirme tamamlandı.";
        }

        private void Tts_OnError(string error)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Tts_OnError(error)));
                return;
            }

            MessageBox.Show(error, "TTS Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            UpdateTtsUIState(SynthesisStatus.Idle);
        }

        #endregion

        #region --- SPEECH-TO-TEXT (STT) İŞLEMLERİ ---

        private void BindSttEvents()
        {
            _sttService.OnStatusChanged += Stt_OnStatusChanged;
            _sttService.OnSpeechRecognized += Stt_OnSpeechRecognized;
            _sttService.OnSpeechHypothesized += Stt_OnSpeechHypothesized;
            _sttService.OnAudioLevelUpdated += Stt_OnAudioLevelUpdated;
            _sttService.OnError += Stt_OnError;

            btnSttStart.Click += (s, e) => _sttService.StartListening();
            btnSttStop.Click += (s, e) => _sttService.StopListening();

            btnSimulateSpeech.Click += async (s, e) =>
            {
                string sample = cmbSamplePhrases.SelectedItem?.ToString() ?? "Merhaba dünya";
                btnSimulateSpeech.Enabled = false;
                await _sttService.SimulateSpeechRecognitionAsync(sample, 0.96f);
                btnSimulateSpeech.Enabled = true;
            };

            // 🗣️ ⇄ 🎙️ Çift Yönlü Entegrasyon: Konuşulanı TTS'e Aktar ve Seslendir
            btnSttTransferToTts.Click += async (s, e) =>
            {
                string textToTransfer = "";

                if (dgvSpeechLogs.SelectedRows.Count > 0)
                {
                    textToTransfer = dgvSpeechLogs.SelectedRows[0].Cells["colText"].Value?.ToString() ?? "";
                }

                if (string.IsNullOrWhiteSpace(textToTransfer))
                {
                    textToTransfer = rtbSttTranscript.Text.Trim();
                }

                if (string.IsNullOrWhiteSpace(textToTransfer))
                {
                    MessageBox.Show("Aktarılacak herhangi bir konuşma metni bulunamadı. Lütfen önce bir konuşma kaydediniz veya listeden bir satır seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // TTS sekmesine geç ve metni aktar
                rtbTtsInput.Text = textToTransfer;
                tabMain.SelectedIndex = 0;
                UpdateTextStatistics();

                lblFooterStatus.Text = "Konuşulan metin TTS sekmesine aktarıldı, seslendiriliyor...";
                await _ttsService.SpeakAsync(textToTransfer);
            };

            btnSttSaveTranscript.Click += (s, e) =>
            {
                if (_sttService.Logs.Count == 0)
                {
                    MessageBox.Show("Kaydedilecek konuşma geçmişi bulunmuyor.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Metin Belgesi (*.txt)|*.txt",
                    FileName = $"STT_Transkript_{DateTime.Now:yyyyMMdd_HHmmss}.txt",
                    Title = "Konuşma Geçmişini TXT Olarak Kaydet"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (_exportService.SaveTranscriptToFile(sfd.FileName, _sttService.Logs))
                    {
                        MessageBox.Show($"Konuşma dökümü başarıyla kaydedildi:\n{sfd.FileName}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Dosya kaydedilirken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };

            btnSttClearLogs.Click += (s, e) =>
            {
                if (_sttService.Logs.Count > 0 || rtbSttTranscript.TextLength > 0)
                {
                    if (MessageBox.Show("Tüm konuşma geçmişini ve transkripti temizlemek istiyor musunuz?", "Temizle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _sttService.ClearLogs();
                        dgvSpeechLogs.Rows.Clear();
                        rtbSttTranscript.Clear();
                        grpSpeechLogs.Text = "🕒 Zaman Damgalı Algılanan Konuşma Geçmişi (0)";
                        lblFooterStatus.Text = "Konuşma geçmişi temizlendi.";
                    }
                }
            };
        }

        private void Stt_OnStatusChanged(RecognitionStatus status, string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Stt_OnStatusChanged(status, message)));
                return;
            }

            UpdateSttUIState(status, message);
        }

        private void UpdateSttUIState(RecognitionStatus status, string message)
        {
            switch (status)
            {
                case RecognitionStatus.Idle:
                    lblSttStatus.Text = "■ Dinleme Kapalı";
                    lblSttStatus.BackColor = Color.FromArgb(100, 116, 139);
                    btnSttStart.Enabled = true;
                    btnSttStop.Enabled = false;
                    lblFooterStatus.Text = "Mikrofon kapalı. Dinlemeyi başlatmak için 'Dinlemeyi Başlat' butonuna basınız.";
                    break;

                case RecognitionStatus.Listening:
                    lblSttStatus.Text = "● Mikrofon Dinleniyor";
                    lblSttStatus.BackColor = Color.FromArgb(22, 163, 74);
                    btnSttStart.Enabled = false;
                    btnSttStop.Enabled = true;
                    lblFooterStatus.Text = "Mikrofon aktif! Konuşabilirsiniz (Örnek komutlar: 'Temizle', 'Kaydet', 'Oku')...";
                    break;

                case RecognitionStatus.Processing:
                    lblSttStatus.Text = "⏳ Ses İşleniyor...";
                    lblSttStatus.BackColor = Color.FromArgb(217, 119, 6);
                    break;

                case RecognitionStatus.Unavailable:
                    lblSttStatus.Text = "⚠ Kullanılamıyor";
                    lblSttStatus.BackColor = Color.FromArgb(220, 38, 38);
                    btnSttStart.Enabled = true;
                    btnSttStop.Enabled = false;
                    lblFooterStatus.Text = "Mikrofon veya SAPI konuşma motoru bulunamadı. Test için simülasyonu kullanabilirsiniz.";
                    break;
            }
        }

        private void Stt_OnSpeechRecognized(SpeechLogEntry entry)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Stt_OnSpeechRecognized(entry)));
                return;
            }

            // Canlı transkript metin kutusuna ekle
            string timeTag = $"[{entry.FormattedTimestamp}] ";
            rtbSttTranscript.SelectionStart = rtbSttTranscript.TextLength;
            rtbSttTranscript.SelectionLength = 0;

            rtbSttTranscript.SelectionColor = Color.FromArgb(100, 116, 139);
            rtbSttTranscript.SelectionFont = new Font(rtbSttTranscript.Font, FontStyle.Regular);
            rtbSttTranscript.AppendText(timeTag);

            rtbSttTranscript.SelectionColor = Color.FromArgb(15, 23, 42);
            rtbSttTranscript.SelectionFont = new Font(rtbSttTranscript.Font, FontStyle.Bold);
            rtbSttTranscript.AppendText($"{entry.Text}\n");
            rtbSttTranscript.ScrollToCaret();

            // Geçmiş tablosuna ekle
            dgvSpeechLogs.Rows.Insert(0, entry.Id, entry.FormattedTimestamp, entry.Source, entry.ConfidencePercentage, entry.Text);
            grpSpeechLogs.Text = $"🕒 Zaman Damgalı Algılanan Konuşma Geçmişi ({_sttService.Logs.Count})";

            lblFooterStatus.Text = $"Algılandı ({entry.ConfidencePercentage}): \"{entry.Text}\"";

            // ⚡ SESLİ KOMUT KONTROLÜ
            CheckVoiceCommands(entry.Text);
        }

        private void CheckVoiceCommands(string text)
        {
            string lower = text.ToLower();

            if (lower.Contains("temizle") || lower.Contains("clear"))
            {
                lblVoiceCommandAlert.Text = "⚡ Sesli Komut: Temizle!";
                btnSttClearLogs.PerformClick();
            }
            else if (lower.Contains("kaydet") || lower.Contains("save"))
            {
                lblVoiceCommandAlert.Text = "⚡ Sesli Komut: Kaydet!";
                btnSttSaveTranscript.PerformClick();
            }
            else if (lower.Contains("durdur") || lower.Contains("stop"))
            {
                lblVoiceCommandAlert.Text = "⚡ Sesli Komut: Durdur!";
                btnSttStop.PerformClick();
            }
            else if (lower.Contains("oku") || lower.Contains("seslendir") || lower.Contains("speak"))
            {
                lblVoiceCommandAlert.Text = "⚡ Sesli Komut: Oku / Seslendir!";
                btnSttTransferToTts.PerformClick();
            }
        }

        private void Stt_OnSpeechHypothesized(string partialText)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Stt_OnSpeechHypothesized(partialText)));
                return;
            }

            lblFooterStatus.Text = $"Canlı konuşma algılanıyor: \"{partialText}\"";
        }

        private void Stt_OnAudioLevelUpdated(int level)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Stt_OnAudioLevelUpdated(level)));
                return;
            }

            prgAudioLevel.Value = Math.Clamp(level, 0, 100);
        }

        private void Stt_OnError(string error)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Stt_OnError(error)));
                return;
            }

            MessageBox.Show(error, "Speech-to-Text Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }
}
