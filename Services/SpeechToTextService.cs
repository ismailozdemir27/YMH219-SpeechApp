using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Threading.Tasks;
using SpeechApp.Enums;
using SpeechApp.Models;

namespace SpeechApp.Services
{
    /// <summary>
    /// Sesten Metne (Speech-to-Text / STT) dönüşümünü yöneten,
    /// mikrofon girişini dinleyen, ses seviyesi (VU) ve güven puanı raporlayan servis.
    /// Recognizer olmayan sistemlerde güvenli hata yönetimi ve simülasyon desteği sunar.
    /// </summary>
    public class SpeechToTextService : IDisposable
    {
        private SpeechRecognitionEngine? _engine;
        private RecognitionStatus _status = RecognitionStatus.Idle;
        private readonly List<SpeechLogEntry> _logs = new List<SpeechLogEntry>();
        private int _logCounter = 0;
        private bool _disposed = false;

        public bool IsRecognizerAvailable { get; private set; } = false;
        public string RecognizerName { get; private set; } = "Yüklü Tanıyıcı Yok";

        public RecognitionStatus Status
        {
            get => _status;
            private set
            {
                if (_status != value)
                {
                    _status = value;
                    OnStatusChanged?.Invoke(_status, GetStatusMessage(_status));
                }
            }
        }

        public IReadOnlyList<SpeechLogEntry> Logs => _logs.AsReadOnly();

        public event Action<SpeechLogEntry>? OnSpeechRecognized;
        public event Action<string>? OnSpeechHypothesized; // Canlı devam eden konuşma tahmini
        public event Action<int>? OnAudioLevelUpdated;     // Mikrofon giriş seviyesi (0-100)
        public event Action<RecognitionStatus, string>? OnStatusChanged;
        public event Action<string>? OnError;

        public SpeechToTextService()
        {
            InitializeEngine();
        }

        private void InitializeEngine()
        {
            try
            {
                var recognizers = SpeechRecognitionEngine.InstalledRecognizers();
                if (recognizers.Count == 0)
                {
                    IsRecognizerAvailable = false;
                    RecognizerName = "Sistemde SAPI Konuşma Tanıyıcı Bulunamadı";
                    Status = RecognitionStatus.Unavailable;
                    return;
                }

                // İlk mevcut tanıyıcıyı al (Varsayılan kültür veya İngilizce/Türkçe)
                RecognizerInfo info = recognizers.FirstOrDefault(r => r.Culture.TwoLetterISOLanguageName == "tr") 
                                      ?? recognizers[0];

                RecognizerName = $"{info.Name} ({info.Culture.DisplayName})";
                _engine = new SpeechRecognitionEngine(info.Id);

                // Temel serbest dikte dilbilgisi (DictationGrammar) yükle
                var dictation = new DictationGrammar();
                dictation.Name = "Genel Dikte";
                _engine.LoadGrammar(dictation);

                // Yaygın Türkçe/İngilizce lab komutları için ek kural tabanlı dilbilgisi
                try
                {
                    var choices = new Choices(new string[] {
                        "merhaba", "selam", "evet", "hayır", "başlat", "durdur",
                        "temizle", "kaydet", "test", "nesne tabanlı programlama",
                        "speech to text", "text to speech", "ismail özdemir"
                    });
                    var grammarBuilder = new GrammarBuilder(choices);
                    _engine.LoadGrammar(new Grammar(grammarBuilder));
                }
                catch { }

                // Olayları bağla
                _engine.SpeechRecognized += Engine_SpeechRecognized;
                _engine.SpeechHypothesized += Engine_SpeechHypothesized;
                _engine.AudioLevelUpdated += Engine_AudioLevelUpdated;
                _engine.RecognizeCompleted += Engine_RecognizeCompleted;

                IsRecognizerAvailable = true;
                Status = RecognitionStatus.Idle;
            }
            catch (Exception ex)
            {
                IsRecognizerAvailable = false;
                Status = RecognitionStatus.Unavailable;
                OnError?.Invoke($"Konuşma tanıma motoru başlatılırken hata oluştu: {ex.Message}");
            }
        }

        /// <summary>
        /// Mikrofonu dinlemeye başlar.
        /// </summary>
        public bool StartListening()
        {
            if (!IsRecognizerAvailable || _engine == null)
            {
                OnError?.Invoke("Sistemde yüklü bir konuşma tanıma motoru (SAPI Recognizer) bulunamadı!\n" +
                               "Mikrofonu test etmek için 'Test Konuşması Simüle Et' özelliğini kullanabilir " +
                               "veya Windows Ayarları > Konuşma bölümünden dil paketi ekleyebilirsiniz.");
                Status = RecognitionStatus.Unavailable;
                return false;
            }

            if (Status == RecognitionStatus.Listening) return true;

            try
            {
                // Varsayılan mikrofon ses aygıtına bağlan
                _engine.SetInputToDefaultAudioDevice();
                _engine.RecognizeAsync(RecognizeMode.Multiple);
                Status = RecognitionStatus.Listening;
                return true;
            }
            catch (Exception ex)
            {
                Status = RecognitionStatus.Unavailable;
                OnError?.Invoke($"Mikrofona erişilemedi: {ex.Message}\nLütfen mikrofonunuzun bağlı ve izinlerinin açık olduğunu kontrol ediniz.");
                return false;
            }
        }

        /// <summary>
        /// Mikrofon dinlemesini durdurur.
        /// </summary>
        public void StopListening()
        {
            if (_engine != null && Status == RecognitionStatus.Listening)
            {
                try
                {
                    _engine.RecognizeAsyncStop();
                    Status = RecognitionStatus.Idle;
                    OnAudioLevelUpdated?.Invoke(0);
                }
                catch (Exception ex)
                {
                    OnError?.Invoke($"Dinleme durdurulurken hata: {ex.Message}");
                }
            }
            else
            {
                Status = RecognitionStatus.Idle;
                OnAudioLevelUpdated?.Invoke(0);
            }
        }

        /// <summary>
        /// Donanım veya dil paketi bulunmayan ortamlarda (laboratuvar sunumu için)
        /// konuşma tanımayı simüle ederek arayüz ve log testinin eksiksiz yapılmasını sağlar.
        /// </summary>
        public async Task SimulateSpeechRecognitionAsync(string sampleText, float simulatedConfidence = 0.94f)
        {
            if (string.IsNullOrWhiteSpace(sampleText)) return;

            Status = RecognitionStatus.Processing;

            // Gerçekçi mikrofon ses dalgalanması simülasyonu
            for (int i = 0; i < 5; i++)
            {
                OnAudioLevelUpdated?.Invoke(new Random().Next(30, 85));
                await Task.Delay(100);
            }

            // Kısmi tahmin bildirimi
            string[] words = sampleText.Split(' ');
            if (words.Length > 1)
            {
                OnSpeechHypothesized?.Invoke(words[0] + "...");
                await Task.Delay(150);
            }

            OnAudioLevelUpdated?.Invoke(0);

            // Nihai tanıma kaydı
            _logCounter++;
            var entry = new SpeechLogEntry
            {
                Id = _logCounter,
                Timestamp = DateTime.Now,
                Text = sampleText.Trim(),
                Confidence = simulatedConfidence,
                Source = IsRecognizerAvailable ? "Mikrofon (Test)" : "Simülasyon (Test)"
            };

            _logs.Add(entry);
            Status = IsRecognizerAvailable && _engine != null ? RecognitionStatus.Listening : RecognitionStatus.Idle;
            OnSpeechRecognized?.Invoke(entry);
        }

        private void Engine_SpeechRecognized(object? sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result == null || string.IsNullOrWhiteSpace(e.Result.Text)) return;

            _logCounter++;
            var entry = new SpeechLogEntry
            {
                Id = _logCounter,
                Timestamp = DateTime.Now,
                Text = e.Result.Text,
                Confidence = e.Result.Confidence,
                Source = "Mikrofon"
            };

            _logs.Add(entry);
            OnSpeechRecognized?.Invoke(entry);
        }

        private void Engine_SpeechHypothesized(object? sender, SpeechHypothesizedEventArgs e)
        {
            if (e.Result != null && !string.IsNullOrWhiteSpace(e.Result.Text))
            {
                OnSpeechHypothesized?.Invoke(e.Result.Text);
            }
        }

        private void Engine_AudioLevelUpdated(object? sender, AudioLevelUpdatedEventArgs e)
        {
            OnAudioLevelUpdated?.Invoke(e.AudioLevel);
        }

        private void Engine_RecognizeCompleted(object? sender, RecognizeCompletedEventArgs e)
        {
            if (Status == RecognitionStatus.Listening)
            {
                Status = RecognitionStatus.Idle;
            }
            OnAudioLevelUpdated?.Invoke(0);
        }

        public void ClearLogs()
        {
            _logs.Clear();
            _logCounter = 0;
        }

        private string GetStatusMessage(RecognitionStatus status)
        {
            return status switch
            {
                RecognitionStatus.Idle => "Hazır (Dinleme Kapalı)",
                RecognitionStatus.Listening => "Mikrofon Dinleniyor...",
                RecognitionStatus.Processing => "Ses İşleniyor...",
                RecognitionStatus.Unavailable => "Mikrofon / Konuşma Servisi Kullanılamıyor",
                _ => "Bilinmiyor"
            };
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                if (_engine != null)
                {
                    try
                    {
                        _engine.RecognizeAsyncCancel();
                        _engine.Dispose();
                    }
                    catch { }
                    _engine = null;
                }
            }
        }
    }
}
