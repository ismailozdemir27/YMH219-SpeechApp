namespace SpeechApp.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabTts;
        private System.Windows.Forms.TabPage tabStt;

        // --- TTS Controls ---
        private System.Windows.Forms.Panel pnlTtsTop;
        private System.Windows.Forms.Label lblVoiceSelect;
        private System.Windows.Forms.ComboBox cmbVoices;
        private System.Windows.Forms.Label lblRate;
        private System.Windows.Forms.TrackBar tbRate;
        private System.Windows.Forms.Label lblRateValue;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.TrackBar tbVolume;
        private System.Windows.Forms.Label lblVolumeValue;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Label lblTtsStatus;
        private System.Windows.Forms.Panel pnlWaveform;
        private System.Windows.Forms.Label[] lblBars;

        private System.Windows.Forms.RichTextBox rtbTtsInput;
        private System.Windows.Forms.Panel pnlTtsStats;
        private System.Windows.Forms.Label lblTtsStats;

        private System.Windows.Forms.Panel pnlTtsBottom;
        private System.Windows.Forms.Button btnTtsSpeak;
        private System.Windows.Forms.Button btnTtsPause;
        private System.Windows.Forms.Button btnTtsResume;
        private System.Windows.Forms.Button btnTtsStop;
        private System.Windows.Forms.Button btnTtsClear;
        private System.Windows.Forms.Button btnTtsOpenFile;
        private System.Windows.Forms.Button btnTtsSaveText;
        private System.Windows.Forms.Button btnTtsSaveWav;

        // --- STT Controls ---
        private System.Windows.Forms.Panel pnlSttTop;
        private System.Windows.Forms.Button btnSttStart;
        private System.Windows.Forms.Button btnSttStop;
        private System.Windows.Forms.Label lblSttStatus;
        private System.Windows.Forms.Label lblAudioLevel;
        private System.Windows.Forms.ProgressBar prgAudioLevel;
        private System.Windows.Forms.Label lblVoiceCommandAlert;
        private System.Windows.Forms.Label lblRecognizerInfo;

        private System.Windows.Forms.SplitContainer splitStt;
        private System.Windows.Forms.GroupBox grpLiveTranscript;
        private System.Windows.Forms.RichTextBox rtbSttTranscript;
        private System.Windows.Forms.GroupBox grpSpeechLogs;
        private System.Windows.Forms.DataGridView dgvSpeechLogs;

        private System.Windows.Forms.Panel pnlSttBottom;
        private System.Windows.Forms.Button btnSttTransferToTts;
        private System.Windows.Forms.Label lblSimLabel;
        private System.Windows.Forms.ComboBox cmbSamplePhrases;
        private System.Windows.Forms.Button btnSimulateSpeech;
        private System.Windows.Forms.Button btnSttSaveTranscript;
        private System.Windows.Forms.Button btnSttClearLogs;

        // --- Footer Status Bar ---
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblFooterStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblFooterStudent;

        private System.Windows.Forms.Timer tmrWaveform;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabTts = new System.Windows.Forms.TabPage();
            this.tabStt = new System.Windows.Forms.TabPage();

            // TTS init
            this.pnlTtsTop = new System.Windows.Forms.Panel();
            this.lblVoiceSelect = new System.Windows.Forms.Label();
            this.cmbVoices = new System.Windows.Forms.ComboBox();
            this.lblRate = new System.Windows.Forms.Label();
            this.tbRate = new System.Windows.Forms.TrackBar();
            this.lblRateValue = new System.Windows.Forms.Label();
            this.lblVolume = new System.Windows.Forms.Label();
            this.tbVolume = new System.Windows.Forms.TrackBar();
            this.lblVolumeValue = new System.Windows.Forms.Label();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.lblTtsStatus = new System.Windows.Forms.Label();
            this.pnlWaveform = new System.Windows.Forms.Panel();

            this.rtbTtsInput = new System.Windows.Forms.RichTextBox();
            this.pnlTtsStats = new System.Windows.Forms.Panel();
            this.lblTtsStats = new System.Windows.Forms.Label();

            this.pnlTtsBottom = new System.Windows.Forms.Panel();
            this.btnTtsSpeak = new System.Windows.Forms.Button();
            this.btnTtsPause = new System.Windows.Forms.Button();
            this.btnTtsResume = new System.Windows.Forms.Button();
            this.btnTtsStop = new System.Windows.Forms.Button();
            this.btnTtsClear = new System.Windows.Forms.Button();
            this.btnTtsOpenFile = new System.Windows.Forms.Button();
            this.btnTtsSaveText = new System.Windows.Forms.Button();
            this.btnTtsSaveWav = new System.Windows.Forms.Button();

            // STT init
            this.pnlSttTop = new System.Windows.Forms.Panel();
            this.btnSttStart = new System.Windows.Forms.Button();
            this.btnSttStop = new System.Windows.Forms.Button();
            this.lblSttStatus = new System.Windows.Forms.Label();
            this.lblAudioLevel = new System.Windows.Forms.Label();
            this.prgAudioLevel = new System.Windows.Forms.ProgressBar();
            this.lblVoiceCommandAlert = new System.Windows.Forms.Label();
            this.lblRecognizerInfo = new System.Windows.Forms.Label();

            this.splitStt = new System.Windows.Forms.SplitContainer();
            this.grpLiveTranscript = new System.Windows.Forms.GroupBox();
            this.rtbSttTranscript = new System.Windows.Forms.RichTextBox();
            this.grpSpeechLogs = new System.Windows.Forms.GroupBox();
            this.dgvSpeechLogs = new System.Windows.Forms.DataGridView();

            this.pnlSttBottom = new System.Windows.Forms.Panel();
            this.btnSttTransferToTts = new System.Windows.Forms.Button();
            this.lblSimLabel = new System.Windows.Forms.Label();
            this.cmbSamplePhrases = new System.Windows.Forms.ComboBox();
            this.btnSimulateSpeech = new System.Windows.Forms.Button();
            this.btnSttSaveTranscript = new System.Windows.Forms.Button();
            this.btnSttClearLogs = new System.Windows.Forms.Button();

            // Status strip
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblFooterStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFooterStudent = new System.Windows.Forms.ToolStripStatusLabel();

            this.tmrWaveform = new System.Windows.Forms.Timer(this.components);

            this.tabMain.SuspendLayout();
            this.tabTts.SuspendLayout();
            this.tabStt.SuspendLayout();
            this.pnlTtsTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
            this.pnlTtsStats.SuspendLayout();
            this.pnlTtsBottom.SuspendLayout();
            this.pnlSttTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitStt)).BeginInit();
            this.splitStt.Panel1.SuspendLayout();
            this.splitStt.Panel2.SuspendLayout();
            this.splitStt.SuspendLayout();
            this.grpLiveTranscript.SuspendLayout();
            this.grpSpeechLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpeechLogs)).BeginInit();
            this.pnlSttBottom.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabTts);
            this.tabMain.Controls.Add(this.tabStt);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tabMain.ItemSize = new System.Drawing.Size(240, 36);
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1080, 660);
            this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabMain.TabIndex = 0;

            // 
            // tabTts
            // 
            this.tabTts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.tabTts.Controls.Add(this.rtbTtsInput);
            this.tabTts.Controls.Add(this.pnlTtsStats);
            this.tabTts.Controls.Add(this.pnlTtsBottom);
            this.tabTts.Controls.Add(this.pnlTtsTop);
            this.tabTts.Location = new System.Drawing.Point(4, 40);
            this.tabTts.Name = "tabTts";
            this.tabTts.Size = new System.Drawing.Size(1072, 616);
            this.tabTts.TabIndex = 0;
            this.tabTts.Text = "🗣️ Metinden Sese (Text-to-Speech)";

            // 
            // pnlTtsTop
            // 
            this.pnlTtsTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlTtsTop.Controls.Add(this.pnlWaveform);
            this.pnlTtsTop.Controls.Add(this.lblTtsStatus);
            this.pnlTtsTop.Controls.Add(this.btnZoomOut);
            this.pnlTtsTop.Controls.Add(this.btnZoomIn);
            this.pnlTtsTop.Controls.Add(this.lblVolumeValue);
            this.pnlTtsTop.Controls.Add(this.tbVolume);
            this.pnlTtsTop.Controls.Add(this.lblVolume);
            this.pnlTtsTop.Controls.Add(this.lblRateValue);
            this.pnlTtsTop.Controls.Add(this.tbRate);
            this.pnlTtsTop.Controls.Add(this.lblRate);
            this.pnlTtsTop.Controls.Add(this.cmbVoices);
            this.pnlTtsTop.Controls.Add(this.lblVoiceSelect);
            this.pnlTtsTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTtsTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTtsTop.Name = "pnlTtsTop";
            this.pnlTtsTop.Size = new System.Drawing.Size(1072, 60);
            this.pnlTtsTop.TabIndex = 0;

            // 
            // lblVoiceSelect
            // 
            this.lblVoiceSelect.AutoSize = true;
            this.lblVoiceSelect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblVoiceSelect.ForeColor = System.Drawing.Color.White;
            this.lblVoiceSelect.Location = new System.Drawing.Point(10, 22);
            this.lblVoiceSelect.Name = "lblVoiceSelect";
            this.lblVoiceSelect.Size = new System.Drawing.Size(70, 15);
            this.lblVoiceSelect.TabIndex = 0;
            this.lblVoiceSelect.Text = "Ses Profili:";

            // 
            // cmbVoices
            // 
            this.cmbVoices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVoices.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbVoices.FormattingEnabled = true;
            this.cmbVoices.Location = new System.Drawing.Point(82, 18);
            this.cmbVoices.Name = "cmbVoices";
            this.cmbVoices.Size = new System.Drawing.Size(275, 23);
            this.cmbVoices.TabIndex = 1;

            // 
            // lblRate
            // 
            this.lblRate.AutoSize = true;
            this.lblRate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRate.ForeColor = System.Drawing.Color.White;
            this.lblRate.Location = new System.Drawing.Point(365, 22);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(29, 15);
            this.lblRate.TabIndex = 2;
            this.lblRate.Text = "Hız:";

            // 
            // tbRate
            // 
            this.tbRate.LargeChange = 2;
            this.tbRate.Location = new System.Drawing.Point(395, 14);
            this.tbRate.Maximum = 10;
            this.tbRate.Minimum = -10;
            this.tbRate.Name = "tbRate";
            this.tbRate.Size = new System.Drawing.Size(95, 45);
            this.tbRate.TabIndex = 3;
            this.tbRate.TickFrequency = 2;
            this.tbRate.Value = 0;

            // 
            // lblRateValue
            // 
            this.lblRateValue.AutoSize = true;
            this.lblRateValue.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblRateValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblRateValue.Location = new System.Drawing.Point(492, 22);
            this.lblRateValue.Name = "lblRateValue";
            this.lblRateValue.Size = new System.Drawing.Size(14, 15);
            this.lblRateValue.TabIndex = 4;
            this.lblRateValue.Text = "0";

            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblVolume.ForeColor = System.Drawing.Color.White;
            this.lblVolume.Location = new System.Drawing.Point(520, 22);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(43, 15);
            this.lblVolume.TabIndex = 5;
            this.lblVolume.Text = "Seviye:";

            // 
            // tbVolume
            // 
            this.tbVolume.LargeChange = 10;
            this.tbVolume.Location = new System.Drawing.Point(565, 14);
            this.tbVolume.Maximum = 100;
            this.tbVolume.Name = "tbVolume";
            this.tbVolume.Size = new System.Drawing.Size(95, 45);
            this.tbVolume.TabIndex = 6;
            this.tbVolume.TickFrequency = 20;
            this.tbVolume.Value = 100;

            // 
            // lblVolumeValue
            // 
            this.lblVolumeValue.AutoSize = true;
            this.lblVolumeValue.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblVolumeValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblVolumeValue.Location = new System.Drawing.Point(662, 22);
            this.lblVolumeValue.Name = "lblVolumeValue";
            this.lblVolumeValue.Size = new System.Drawing.Size(37, 15);
            this.lblVolumeValue.TabIndex = 7;
            this.lblVolumeValue.Text = "%100";

            // 
            // btnZoomIn
            // 
            this.btnZoomIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnZoomIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZoomIn.FlatAppearance.BorderSize = 0;
            this.btnZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomIn.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnZoomIn.ForeColor = System.Drawing.Color.White;
            this.btnZoomIn.Location = new System.Drawing.Point(710, 16);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(42, 28);
            this.btnZoomIn.TabIndex = 8;
            this.btnZoomIn.Text = "A+";
            this.btnZoomIn.UseVisualStyleBackColor = false;

            // 
            // btnZoomOut
            // 
            this.btnZoomOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnZoomOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZoomOut.FlatAppearance.BorderSize = 0;
            this.btnZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomOut.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnZoomOut.ForeColor = System.Drawing.Color.White;
            this.btnZoomOut.Location = new System.Drawing.Point(756, 16);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(42, 28);
            this.btnZoomOut.TabIndex = 9;
            this.btnZoomOut.Text = "A-";
            this.btnZoomOut.UseVisualStyleBackColor = false;

            // 
            // pnlWaveform
            // 
            this.pnlWaveform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlWaveform.Location = new System.Drawing.Point(820, 16);
            this.pnlWaveform.Name = "pnlWaveform";
            this.pnlWaveform.Size = new System.Drawing.Size(90, 28);
            this.pnlWaveform.TabIndex = 10;

            // 
            // lblTtsStatus
            // 
            this.lblTtsStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTtsStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblTtsStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTtsStatus.ForeColor = System.Drawing.Color.White;
            this.lblTtsStatus.Location = new System.Drawing.Point(920, 16);
            this.lblTtsStatus.Name = "lblTtsStatus";
            this.lblTtsStatus.Size = new System.Drawing.Size(142, 28);
            this.lblTtsStatus.TabIndex = 11;
            this.lblTtsStatus.Text = "● Hazır";
            this.lblTtsStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // rtbTtsInput
            // 
            this.rtbTtsInput.AllowDrop = true;
            this.rtbTtsInput.BackColor = System.Drawing.Color.White;
            this.rtbTtsInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbTtsInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbTtsInput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular);
            this.rtbTtsInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.rtbTtsInput.Location = new System.Drawing.Point(0, 60);
            this.rtbTtsInput.Name = "rtbTtsInput";
            this.rtbTtsInput.Size = new System.Drawing.Size(1072, 474);
            this.rtbTtsInput.TabIndex = 1;
            this.rtbTtsInput.Text = "Merhaba! Bu uygulama, Nesne Tabanlı Programlama (YMH 219) dersi kapsamında geliştirilmiş Speech-to-Text ve Text-to-Speech masaüstü yazılımıdır.\n\nİstediğiniz metni buraya yazabilir veya bilgisayarınızdan bir .txt metin belgesini sürükleyip bırakabilirsiniz.\n\nTürkçe ses motoru (Microsoft Tolga) ve İngilizce ses profilleri ile akıcı bir şekilde seslendirme yapabilir, hız ve ses düzeyini anlık ayarlayabilirsiniz.";

            // 
            // pnlTtsStats
            // 
            this.pnlTtsStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlTtsStats.Controls.Add(this.lblTtsStats);
            this.pnlTtsStats.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTtsStats.Location = new System.Drawing.Point(0, 534);
            this.pnlTtsStats.Name = "pnlTtsStats";
            this.pnlTtsStats.Size = new System.Drawing.Size(1072, 28);
            this.pnlTtsStats.TabIndex = 2;

            // 
            // lblTtsStats
            // 
            this.lblTtsStats.AutoSize = true;
            this.lblTtsStats.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular);
            this.lblTtsStats.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTtsStats.Location = new System.Drawing.Point(12, 6);
            this.lblTtsStats.Name = "lblTtsStats";
            this.lblTtsStats.Size = new System.Drawing.Size(370, 15);
            this.lblTtsStats.TabIndex = 0;
            this.lblTtsStats.Text = "📊 Karakter: 0 | Kelime: 0 | Cümle: 0 | ⏱️ Tahmini Okuma Süresi: ~0 sn";

            // 
            // pnlTtsBottom
            // 
            this.pnlTtsBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlTtsBottom.Controls.Add(this.btnTtsSaveWav);
            this.pnlTtsBottom.Controls.Add(this.btnTtsSaveText);
            this.pnlTtsBottom.Controls.Add(this.btnTtsOpenFile);
            this.pnlTtsBottom.Controls.Add(this.btnTtsClear);
            this.pnlTtsBottom.Controls.Add(this.btnTtsStop);
            this.pnlTtsBottom.Controls.Add(this.btnTtsResume);
            this.pnlTtsBottom.Controls.Add(this.btnTtsPause);
            this.pnlTtsBottom.Controls.Add(this.btnTtsSpeak);
            this.pnlTtsBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTtsBottom.Location = new System.Drawing.Point(0, 562);
            this.pnlTtsBottom.Name = "pnlTtsBottom";
            this.pnlTtsBottom.Size = new System.Drawing.Size(1072, 54);
            this.pnlTtsBottom.TabIndex = 3;

            // 
            // btnTtsSpeak
            // 
            this.btnTtsSpeak.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnTtsSpeak.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTtsSpeak.FlatAppearance.BorderSize = 0;
            this.btnTtsSpeak.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTtsSpeak.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTtsSpeak.ForeColor = System.Drawing.Color.White;
            this.btnTtsSpeak.Location = new System.Drawing.Point(10, 10);
            this.btnTtsSpeak.Name = "btnTtsSpeak";
            this.btnTtsSpeak.Size = new System.Drawing.Size(105, 34);
            this.btnTtsSpeak.TabIndex = 0;
            this.btnTtsSpeak.Text = "▶ Oynat";
            this.btnTtsSpeak.UseVisualStyleBackColor = false;

            // 
            // btnTtsPause
            // 
            this.btnTtsPause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.btnTtsPause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTtsPause.Enabled = false;
            this.btnTtsPause.FlatAppearance.BorderSize = 0;
            this.btnTtsPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTtsPause.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTtsPause.ForeColor = System.Drawing.Color.White;
            this.btnTtsPause.Location = new System.Drawing.Point(122, 10);
            this.btnTtsPause.Name = "btnTtsPause";
            this.btnTtsPause.Size = new System.Drawing.Size(105, 34);
            this.btnTtsPause.TabIndex = 1;
            this.btnTtsPause.Text = "⏸ Duraklat";
            this.btnTtsPause.UseVisualStyleBackColor = false;

            // 
            // btnTtsResume
            // 
            this.btnTtsResume.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.btnTtsResume.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTtsResume.Enabled = false;
            this.btnTtsResume.FlatAppearance.BorderSize = 0;
            this.btnTtsResume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTtsResume.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTtsResume.ForeColor = System.Drawing.Color.White;
            this.btnTtsResume.Location = new System.Drawing.Point(234, 10);
            this.btnTtsResume.Name = "btnTtsResume";
            this.btnTtsResume.Size = new System.Drawing.Size(105, 34);
            this.btnTtsResume.TabIndex = 2;
            this.btnTtsResume.Text = "▶ Devam";
            this.btnTtsResume.UseVisualStyleBackColor = false;

            // 
            // btnTtsStop
            // 
            this.btnTtsStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnTtsStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTtsStop.Enabled = false;
            this.btnTtsStop.FlatAppearance.BorderSize = 0;
            this.btnTtsStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTtsStop.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTtsStop.ForeColor = System.Drawing.Color.White;
            this.btnTtsStop.Location = new System.Drawing.Point(346, 10);
            this.btnTtsStop.Name = "btnTtsStop";
            this.btnTtsStop.Size = new System.Drawing.Size(95, 34);
            this.btnTtsStop.TabIndex = 3;
            this.btnTtsStop.Text = "⏹ Durdur";
            this.btnTtsStop.UseVisualStyleBackColor = false;

            // 
            // btnTtsClear
            // 
            this.btnTtsClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnTtsClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTtsClear.FlatAppearance.BorderSize = 0;
            this.btnTtsClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTtsClear.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTtsClear.ForeColor = System.Drawing.Color.White;
            this.btnTtsClear.Location = new System.Drawing.Point(448, 10);
            this.btnTtsClear.Name = "btnTtsClear";
            this.btnTtsClear.Size = new System.Drawing.Size(95, 34);
            this.btnTtsClear.TabIndex = 4;
            this.btnTtsClear.Text = "🗑️ Temizle";
            this.btnTtsClear.UseVisualStyleBackColor = false;

            // 
            // btnTtsOpenFile
            // 
            this.btnTtsOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTtsOpenFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.btnTtsOpenFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTtsOpenFile.FlatAppearance.BorderSize = 0;
            this.btnTtsOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTtsOpenFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTtsOpenFile.ForeColor = System.Drawing.Color.White;
            this.btnTtsOpenFile.Location = new System.Drawing.Point(630, 10);
            this.btnTtsOpenFile.Name = "btnTtsOpenFile";
            this.btnTtsOpenFile.Size = new System.Drawing.Size(135, 34);
            this.btnTtsOpenFile.TabIndex = 5;
            this.btnTtsOpenFile.Text = "📂 Dosyadan Yükle";
            this.btnTtsOpenFile.UseVisualStyleBackColor = false;

            // 
            // btnTtsSaveText
            // 
            this.btnTtsSaveText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTtsSaveText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnTtsSaveText.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTtsSaveText.FlatAppearance.BorderSize = 0;
            this.btnTtsSaveText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTtsSaveText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTtsSaveText.ForeColor = System.Drawing.Color.White;
            this.btnTtsSaveText.Location = new System.Drawing.Point(772, 10);
            this.btnTtsSaveText.Name = "btnTtsSaveText";
            this.btnTtsSaveText.Size = new System.Drawing.Size(140, 34);
            this.btnTtsSaveText.TabIndex = 6;
            this.btnTtsSaveText.Text = "💾 Metni Kaydet (.txt)";
            this.btnTtsSaveText.UseVisualStyleBackColor = false;

            // 
            // btnTtsSaveWav
            // 
            this.btnTtsSaveWav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTtsSaveWav.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(40)))), ((int)(((byte)(217)))));
            this.btnTtsSaveWav.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTtsSaveWav.FlatAppearance.BorderSize = 0;
            this.btnTtsSaveWav.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTtsSaveWav.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTtsSaveWav.ForeColor = System.Drawing.Color.White;
            this.btnTtsSaveWav.Location = new System.Drawing.Point(920, 10);
            this.btnTtsSaveWav.Name = "btnTtsSaveWav";
            this.btnTtsSaveWav.Size = new System.Drawing.Size(142, 34);
            this.btnTtsSaveWav.TabIndex = 7;
            this.btnTtsSaveWav.Text = "🎵 Sesi Kaydet (.wav)";
            this.btnTtsSaveWav.UseVisualStyleBackColor = false;

            // 
            // tabStt
            // 
            this.tabStt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.tabStt.Controls.Add(this.splitStt);
            this.tabStt.Controls.Add(this.pnlSttBottom);
            this.tabStt.Controls.Add(this.pnlSttTop);
            this.tabStt.Location = new System.Drawing.Point(4, 40);
            this.tabStt.Name = "tabStt";
            this.tabStt.Size = new System.Drawing.Size(1072, 616);
            this.tabStt.TabIndex = 1;
            this.tabStt.Text = "🎙️ Sesten Metne (Speech-to-Text)";

            // 
            // pnlSttTop
            // 
            this.pnlSttTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlSttTop.Controls.Add(this.lblVoiceCommandAlert);
            this.pnlSttTop.Controls.Add(this.lblRecognizerInfo);
            this.pnlSttTop.Controls.Add(this.prgAudioLevel);
            this.pnlSttTop.Controls.Add(this.lblAudioLevel);
            this.pnlSttTop.Controls.Add(this.lblSttStatus);
            this.pnlSttTop.Controls.Add(this.btnSttStop);
            this.pnlSttTop.Controls.Add(this.btnSttStart);
            this.pnlSttTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSttTop.Location = new System.Drawing.Point(0, 0);
            this.pnlSttTop.Name = "pnlSttTop";
            this.pnlSttTop.Size = new System.Drawing.Size(1072, 60);
            this.pnlSttTop.TabIndex = 0;

            // 
            // btnSttStart
            // 
            this.btnSttStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnSttStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSttStart.FlatAppearance.BorderSize = 0;
            this.btnSttStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSttStart.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSttStart.ForeColor = System.Drawing.Color.White;
            this.btnSttStart.Location = new System.Drawing.Point(12, 13);
            this.btnSttStart.Name = "btnSttStart";
            this.btnSttStart.Size = new System.Drawing.Size(155, 34);
            this.btnSttStart.TabIndex = 0;
            this.btnSttStart.Text = "▶ Dinlemeyi Başlat";
            this.btnSttStart.UseVisualStyleBackColor = false;

            // 
            // btnSttStop
            // 
            this.btnSttStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnSttStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSttStop.Enabled = false;
            this.btnSttStop.FlatAppearance.BorderSize = 0;
            this.btnSttStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSttStop.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSttStop.ForeColor = System.Drawing.Color.White;
            this.btnSttStop.Location = new System.Drawing.Point(175, 13);
            this.btnSttStop.Name = "btnSttStop";
            this.btnSttStop.Size = new System.Drawing.Size(155, 34);
            this.btnSttStop.TabIndex = 1;
            this.btnSttStop.Text = "⏹ Dinlemeyi Durdur";
            this.btnSttStop.UseVisualStyleBackColor = false;

            // 
            // lblSttStatus
            // 
            this.lblSttStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSttStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSttStatus.ForeColor = System.Drawing.Color.White;
            this.lblSttStatus.Location = new System.Drawing.Point(340, 16);
            this.lblSttStatus.Name = "lblSttStatus";
            this.lblSttStatus.Size = new System.Drawing.Size(150, 28);
            this.lblSttStatus.TabIndex = 2;
            this.lblSttStatus.Text = "■ Dinleme Kapalı";
            this.lblSttStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // lblAudioLevel
            // 
            this.lblAudioLevel.AutoSize = true;
            this.lblAudioLevel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAudioLevel.ForeColor = System.Drawing.Color.White;
            this.lblAudioLevel.Location = new System.Drawing.Point(500, 22);
            this.lblAudioLevel.Name = "lblAudioLevel";
            this.lblAudioLevel.Size = new System.Drawing.Size(65, 15);
            this.lblAudioLevel.TabIndex = 3;
            this.lblAudioLevel.Text = "Ses Düzeyi:";

            // 
            // prgAudioLevel
            // 
            this.prgAudioLevel.Location = new System.Drawing.Point(570, 20);
            this.prgAudioLevel.Name = "prgAudioLevel";
            this.prgAudioLevel.Size = new System.Drawing.Size(100, 20);
            this.prgAudioLevel.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgAudioLevel.TabIndex = 4;

            // 
            // lblVoiceCommandAlert
            // 
            this.lblVoiceCommandAlert.AutoSize = true;
            this.lblVoiceCommandAlert.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblVoiceCommandAlert.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(191)))), ((int)(((byte)(36)))));
            this.lblVoiceCommandAlert.Location = new System.Drawing.Point(685, 22);
            this.lblVoiceCommandAlert.Name = "lblVoiceCommandAlert";
            this.lblVoiceCommandAlert.Size = new System.Drawing.Size(109, 15);
            this.lblVoiceCommandAlert.TabIndex = 5;
            this.lblVoiceCommandAlert.Text = "⚡ Sesli Komut Modu";

            // 
            // lblRecognizerInfo
            // 
            this.lblRecognizerInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecognizerInfo.AutoEllipsis = true;
            this.lblRecognizerInfo.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblRecognizerInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblRecognizerInfo.Location = new System.Drawing.Point(810, 16);
            this.lblRecognizerInfo.Name = "lblRecognizerInfo";
            this.lblRecognizerInfo.Size = new System.Drawing.Size(250, 28);
            this.lblRecognizerInfo.TabIndex = 6;
            this.lblRecognizerInfo.Text = "Motor: Taranıyor...";
            this.lblRecognizerInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // splitStt
            // 
            this.splitStt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitStt.Location = new System.Drawing.Point(0, 60);
            this.splitStt.Name = "splitStt";
            this.splitStt.Orientation = System.Windows.Forms.Orientation.Horizontal;

            // 
            // splitStt.Panel1
            // 
            this.splitStt.Panel1.Controls.Add(this.grpLiveTranscript);
            this.splitStt.Panel1.Padding = new System.Windows.Forms.Padding(10, 10, 10, 5);

            // 
            // splitStt.Panel2
            // 
            this.splitStt.Panel2.Controls.Add(this.grpSpeechLogs);
            this.splitStt.Panel2.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.splitStt.Size = new System.Drawing.Size(1072, 502);
            this.splitStt.SplitterDistance = 180;
            this.splitStt.TabIndex = 1;

            // 
            // grpLiveTranscript
            // 
            this.grpLiveTranscript.Controls.Add(this.rtbSttTranscript);
            this.grpLiveTranscript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLiveTranscript.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.grpLiveTranscript.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grpLiveTranscript.Location = new System.Drawing.Point(10, 10);
            this.grpLiveTranscript.Name = "grpLiveTranscript";
            this.grpLiveTranscript.Size = new System.Drawing.Size(1052, 165);
            this.grpLiveTranscript.TabIndex = 0;
            this.grpLiveTranscript.TabStop = false;
            this.grpLiveTranscript.Text = "📝 Canlı Konuşma Transkript Metni";

            // 
            // rtbSttTranscript
            // 
            this.rtbSttTranscript.BackColor = System.Drawing.Color.White;
            this.rtbSttTranscript.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbSttTranscript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbSttTranscript.Font = new System.Drawing.Font("Segoe UI", 11.5F);
            this.rtbSttTranscript.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.rtbSttTranscript.Location = new System.Drawing.Point(3, 21);
            this.rtbSttTranscript.Name = "rtbSttTranscript";
            this.rtbSttTranscript.ReadOnly = true;
            this.rtbSttTranscript.Size = new System.Drawing.Size(1046, 141);
            this.rtbSttTranscript.TabIndex = 0;
            this.rtbSttTranscript.Text = "";

            // 
            // grpSpeechLogs
            // 
            this.grpSpeechLogs.Controls.Add(this.dgvSpeechLogs);
            this.grpSpeechLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSpeechLogs.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.grpSpeechLogs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grpSpeechLogs.Location = new System.Drawing.Point(10, 5);
            this.grpSpeechLogs.Name = "grpSpeechLogs";
            this.grpSpeechLogs.Size = new System.Drawing.Size(1052, 308);
            this.grpSpeechLogs.TabIndex = 0;
            this.grpSpeechLogs.TabStop = false;
            this.grpSpeechLogs.Text = "🕒 Zaman Damgalı Algılanan Konuşma Geçmişi (0)";

            // 
            // dgvSpeechLogs
            // 
            this.dgvSpeechLogs.AllowUserToAddRows = false;
            this.dgvSpeechLogs.AllowUserToDeleteRows = false;
            this.dgvSpeechLogs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSpeechLogs.BackgroundColor = System.Drawing.Color.White;
            this.dgvSpeechLogs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSpeechLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpeechLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSpeechLogs.Location = new System.Drawing.Point(3, 21);
            this.dgvSpeechLogs.Name = "dgvSpeechLogs";
            this.dgvSpeechLogs.ReadOnly = true;
            this.dgvSpeechLogs.RowHeadersVisible = false;
            this.dgvSpeechLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSpeechLogs.Size = new System.Drawing.Size(1046, 284);
            this.dgvSpeechLogs.TabIndex = 0;

            // 
            // pnlSttBottom
            // 
            this.pnlSttBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlSttBottom.Controls.Add(this.btnSttTransferToTts);
            this.pnlSttBottom.Controls.Add(this.btnSttClearLogs);
            this.pnlSttBottom.Controls.Add(this.btnSttSaveTranscript);
            this.pnlSttBottom.Controls.Add(this.btnSimulateSpeech);
            this.pnlSttBottom.Controls.Add(this.cmbSamplePhrases);
            this.pnlSttBottom.Controls.Add(this.lblSimLabel);
            this.pnlSttBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSttBottom.Location = new System.Drawing.Point(0, 562);
            this.pnlSttBottom.Name = "pnlSttBottom";
            this.pnlSttBottom.Size = new System.Drawing.Size(1072, 54);
            this.pnlSttBottom.TabIndex = 2;

            // 
            // btnSttTransferToTts
            // 
            this.btnSttTransferToTts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.btnSttTransferToTts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSttTransferToTts.FlatAppearance.BorderSize = 0;
            this.btnSttTransferToTts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSttTransferToTts.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSttTransferToTts.ForeColor = System.Drawing.Color.White;
            this.btnSttTransferToTts.Location = new System.Drawing.Point(10, 10);
            this.btnSttTransferToTts.Name = "btnSttTransferToTts";
            this.btnSttTransferToTts.Size = new System.Drawing.Size(200, 34);
            this.btnSttTransferToTts.TabIndex = 0;
            this.btnSttTransferToTts.Text = "🗣️ ⇄ 🎙️ Konuşulanı TTS ile Oku";
            this.btnSttTransferToTts.UseVisualStyleBackColor = false;

            // 
            // lblSimLabel
            // 
            this.lblSimLabel.AutoSize = true;
            this.lblSimLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSimLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSimLabel.Location = new System.Drawing.Point(220, 19);
            this.lblSimLabel.Name = "lblSimLabel";
            this.lblSimLabel.Size = new System.Drawing.Size(73, 15);
            this.lblSimLabel.TabIndex = 1;
            this.lblSimLabel.Text = "🧪 Test / Sim:";

            // 
            // cmbSamplePhrases
            // 
            this.cmbSamplePhrases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSamplePhrases.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbSamplePhrases.FormattingEnabled = true;
            this.cmbSamplePhrases.Location = new System.Drawing.Point(298, 15);
            this.cmbSamplePhrases.Name = "cmbSamplePhrases";
            this.cmbSamplePhrases.Size = new System.Drawing.Size(270, 23);
            this.cmbSamplePhrases.TabIndex = 2;

            // 
            // btnSimulateSpeech
            // 
            this.btnSimulateSpeech.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.btnSimulateSpeech.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSimulateSpeech.FlatAppearance.BorderSize = 0;
            this.btnSimulateSpeech.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimulateSpeech.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSimulateSpeech.ForeColor = System.Drawing.Color.White;
            this.btnSimulateSpeech.Location = new System.Drawing.Point(575, 10);
            this.btnSimulateSpeech.Name = "btnSimulateSpeech";
            this.btnSimulateSpeech.Size = new System.Drawing.Size(150, 34);
            this.btnSimulateSpeech.TabIndex = 3;
            this.btnSimulateSpeech.Text = "⚡ Test Simüle Et";
            this.btnSimulateSpeech.UseVisualStyleBackColor = false;

            // 
            // btnSttSaveTranscript
            // 
            this.btnSttSaveTranscript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSttSaveTranscript.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnSttSaveTranscript.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSttSaveTranscript.FlatAppearance.BorderSize = 0;
            this.btnSttSaveTranscript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSttSaveTranscript.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSttSaveTranscript.ForeColor = System.Drawing.Color.White;
            this.btnSttSaveTranscript.Location = new System.Drawing.Point(745, 10);
            this.btnSttSaveTranscript.Name = "btnSttSaveTranscript";
            this.btnSttSaveTranscript.Size = new System.Drawing.Size(175, 34);
            this.btnSttSaveTranscript.TabIndex = 4;
            this.btnSttSaveTranscript.Text = "💾 Transkripti Kaydet (.txt)";
            this.btnSttSaveTranscript.UseVisualStyleBackColor = false;

            // 
            // btnSttClearLogs
            // 
            this.btnSttClearLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSttClearLogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnSttClearLogs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSttClearLogs.FlatAppearance.BorderSize = 0;
            this.btnSttClearLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSttClearLogs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSttClearLogs.ForeColor = System.Drawing.Color.White;
            this.btnSttClearLogs.Location = new System.Drawing.Point(928, 10);
            this.btnSttClearLogs.Name = "btnSttClearLogs";
            this.btnSttClearLogs.Size = new System.Drawing.Size(134, 34);
            this.btnSttClearLogs.TabIndex = 5;
            this.btnSttClearLogs.Text = "🗑️ Geçmişi Temizle";
            this.btnSttClearLogs.UseVisualStyleBackColor = false;

            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblFooterStatus,
            this.lblFooterStudent});
            this.statusStrip.Location = new System.Drawing.Point(0, 660);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1080, 24);
            this.statusStrip.TabIndex = 1;

            // 
            // lblFooterStatus
            // 
            this.lblFooterStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFooterStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblFooterStatus.Name = "lblFooterStatus";
            this.lblFooterStatus.Size = new System.Drawing.Size(260, 19);
            this.lblFooterStatus.Text = "Hazır | Speech-to-Text & Text-to-Speech Engine";

            // 
            // lblFooterStudent
            // 
            this.lblFooterStudent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFooterStudent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblFooterStudent.Name = "lblFooterStudent";
            this.lblFooterStudent.Size = new System.Drawing.Size(805, 19);
            this.lblFooterStudent.Spring = true;
            this.lblFooterStudent.Text = "Öğrenci: İsmail Özdemir (250541109) - YMH 219";
            this.lblFooterStudent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 684);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.statusStrip);
            this.MinimumSize = new System.Drawing.Size(980, 620);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YMH219 - Metinden Sese ve Sesten Metne Uygulaması (SpeechApp)";
            this.tabMain.ResumeLayout(false);
            this.tabTts.ResumeLayout(false);
            this.tabStt.ResumeLayout(false);
            this.pnlTtsTop.ResumeLayout(false);
            this.pnlTtsTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
            this.pnlTtsStats.ResumeLayout(false);
            this.pnlTtsStats.PerformLayout();
            this.pnlTtsBottom.ResumeLayout(false);
            this.pnlSttTop.ResumeLayout(false);
            this.pnlSttTop.PerformLayout();
            this.splitStt.Panel1.ResumeLayout(false);
            this.splitStt.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitStt)).EndInit();
            this.splitStt.ResumeLayout(false);
            this.grpLiveTranscript.ResumeLayout(false);
            this.grpSpeechLogs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpeechLogs)).EndInit();
            this.pnlSttBottom.ResumeLayout(false);
            this.pnlSttBottom.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
