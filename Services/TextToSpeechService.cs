using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Media.SpeechSynthesis;
using SpeechApp.Enums;
using SpeechApp.Models;

namespace SpeechApp.Services
{
    /// <summary>
    /// Metinden Sese (Text-to-Speech / TTS) dönüşümünü yöneten ana servis.
    /// Hem klasik SAPI 5 seslerini (Zira vb.) hem de modern Windows OneCore seslerini
    /// (Türkçe Microsoft Tolga vb.) birleşik mimaride destekler.
    /// </summary>
    public class TextToSpeechService : IDisposable
    {
        // --- SAPI 5 Motoru ---
        private System.Speech.Synthesis.SpeechSynthesizer? _sapiSynthesizer;

        // --- OneCore (Modern Windows & Türkçe Tolga) Motoru ---
        private Windows.Media.SpeechSynthesis.SpeechSynthesizer? _oneCoreSynthesizer;
        private MediaPlayer? _mediaPlayer;

        private VoiceModel? _selectedVoice;
        private SynthesisStatus _status = SynthesisStatus.Idle;
        private int _rate = 0; // -10 ile +10
        private int _volume = 100; // 0 ile 100
        private bool _disposed = false;

        public SynthesisStatus Status
        {
            get => _status;
            private set
            {
                if (_status != value)
                {
                    _status = value;
                    OnStatusChanged?.Invoke(_status);
                }
            }
        }

        public int Rate
        {
            get => _rate;
            set
            {
                _rate = Math.Clamp(value, -10, 10);
                if (_sapiSynthesizer != null)
                {
                    _sapiSynthesizer.Rate = _rate;
                }
                if (_mediaPlayer != null)
                {
                    _mediaPlayer.PlaybackSession.PlaybackRate = RateToPlaybackSpeed(_rate);
                }
            }
        }

        public int Volume
        {
            get => _volume;
            set
            {
                _volume = Math.Clamp(value, 0, 100);
                if (_sapiSynthesizer != null)
                {
                    _sapiSynthesizer.Volume = _volume;
                }
                if (_mediaPlayer != null)
                {
                    _mediaPlayer.Volume = _volume / 100.0;
                }
            }
        }

        public VoiceModel? SelectedVoice => _selectedVoice;

        public event Action<SynthesisStatus>? OnStatusChanged;
        public event Action<int, int, string>? OnSpeakProgress; // charPos, charCount, word
        public event Action? OnSpeakCompleted;
        public event Action<string>? OnError;

        public TextToSpeechService()
        {
            InitializeSapi();
            InitializeOneCore();
        }

        private void InitializeSapi()
        {
            try
            {
                _sapiSynthesizer = new System.Speech.Synthesis.SpeechSynthesizer();
                _sapiSynthesizer.SpeakProgress += (s, e) =>
                {
                    OnSpeakProgress?.Invoke(e.CharacterPosition, e.CharacterCount, e.Text ?? string.Empty);
                };
                _sapiSynthesizer.SpeakCompleted += (s, e) =>
                {
                    Status = SynthesisStatus.Idle;
                    if (e.Error != null)
                    {
                        OnError?.Invoke($"Seslendirme hatası: {e.Error.Message}");
                    }
                    else
                    {
                        OnSpeakCompleted?.Invoke();
                    }
                };
                _sapiSynthesizer.StateChanged += (s, e) =>
                {
                    switch (e.State)
                    {
                        case SynthesizerState.Speaking:
                            Status = SynthesisStatus.Speaking;
                            break;
                        case SynthesizerState.Paused:
                            Status = SynthesisStatus.Paused;
                            break;
                        case SynthesizerState.Ready:
                            Status = SynthesisStatus.Idle;
                            break;
                    }
                };
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"SAPI konuşma motoru başlatılamadı: {ex.Message}");
            }
        }

        private void InitializeOneCore()
        {
            try
            {
                _oneCoreSynthesizer = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
                _mediaPlayer = new MediaPlayer
                {
                    AutoPlay = true,
                    Volume = _volume / 100.0
                };

                _mediaPlayer.MediaEnded += (s, e) =>
                {
                    Status = SynthesisStatus.Idle;
                    OnSpeakCompleted?.Invoke();
                };

                _mediaPlayer.MediaFailed += (s, e) =>
                {
                    Status = SynthesisStatus.Idle;
                    OnError?.Invoke($"OneCore ses çalma hatası: {e.ErrorMessage}");
                };
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"OneCore motoru başlatılamadı: {ex.Message}");
            }
        }

        /// <summary>
        /// Sistemde bulunan tüm sesleri (Türkçe OneCore Tolga ve SAPI sesleri dahil) listeler.
        /// Türkçe sesler listenin en başında yer alır.
        /// </summary>
        public List<VoiceModel> GetAvailableVoices()
        {
            var list = new List<VoiceModel>();

            // 1. OneCore Modern Sesleri (Türkçe Tolga vb.)
            try
            {
                foreach (VoiceInformation v in Windows.Media.SpeechSynthesis.SpeechSynthesizer.AllVoices)
                {
                    CultureInfo culture;
                    try { culture = new CultureInfo(v.Language); }
                    catch { culture = CultureInfo.InvariantCulture; }

                    list.Add(new VoiceModel
                    {
                        Name = v.DisplayName,
                        Culture = culture,
                        Gender = v.Gender.ToString(),
                        Age = "Adult",
                        Enabled = true,
                        Engine = VoiceEngine.OneCore,
                        VoiceId = v.Id
                    });
                }
            }
            catch { }

            // 2. SAPI 5 Sesleri
            if (_sapiSynthesizer != null)
            {
                try
                {
                    foreach (InstalledVoice voice in _sapiSynthesizer.GetInstalledVoices())
                    {
                        // OneCore ile mükerrer olanları filtrele
                        if (voice.VoiceInfo != null && list.Any(x => x.Name.Equals(voice.VoiceInfo.Name, StringComparison.OrdinalIgnoreCase)))
                            continue;

                        list.Add(new VoiceModel
                        {
                            Name = voice.VoiceInfo?.Name ?? "SAPI Ses",
                            Culture = voice.VoiceInfo?.Culture ?? CultureInfo.InvariantCulture,
                            Gender = voice.VoiceInfo?.Gender.ToString() ?? "Belirtilmemiş",
                            Age = voice.VoiceInfo?.Age.ToString() ?? "Belirtilmemiş",
                            Enabled = voice.Enabled,
                            Engine = VoiceEngine.Sapi5,
                            VoiceId = voice.VoiceInfo?.Name ?? string.Empty
                        });
                    }
                }
                catch { }
            }

            // Türkçe sesleri en başa al, ardından diğer diller
            return list.OrderByDescending(v => v.Culture.TwoLetterISOLanguageName.Equals("tr", StringComparison.OrdinalIgnoreCase))
                       .ThenBy(v => v.DisplayName)
                       .ToList();
        }

        public bool SelectVoice(VoiceModel voice)
        {
            if (voice == null) return false;
            _selectedVoice = voice;

            try
            {
                if (voice.Engine == VoiceEngine.OneCore && _oneCoreSynthesizer != null)
                {
                    var target = Windows.Media.SpeechSynthesis.SpeechSynthesizer.AllVoices.FirstOrDefault(v => v.Id == voice.VoiceId || v.DisplayName == voice.Name);
                    if (target != null)
                    {
                        _oneCoreSynthesizer.Voice = target;
                        return true;
                    }
                }
                else if (voice.Engine == VoiceEngine.Sapi5 && _sapiSynthesizer != null)
                {
                    _sapiSynthesizer.SelectVoice(voice.Name);
                    return true;
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Ses profili seçilemedi: {ex.Message}");
            }
            return false;
        }

        /// <summary>
        /// Metni seçili motor ile (SAPI veya Türkçe OneCore) seslendirir.
        /// </summary>
        public async Task<bool> SpeakAsync(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return false;

            Stop();

            if (_selectedVoice == null)
            {
                var voices = GetAvailableVoices();
                if (voices.Count > 0) SelectVoice(voices[0]);
            }

            if (_selectedVoice == null)
            {
                OnError?.Invoke("Sistemde kullanılabilir ses profili bulunamadı.");
                return false;
            }

            try
            {
                Status = SynthesisStatus.Speaking;

                if (_selectedVoice.Engine == VoiceEngine.OneCore && _oneCoreSynthesizer != null && _mediaPlayer != null)
                {
                    // OneCore (Türkçe Tolga) seslendirmesi
                    SpeechSynthesisStream stream = await _oneCoreSynthesizer.SynthesizeTextToStreamAsync(text);
                    _mediaPlayer.Source = MediaSource.CreateFromStream(stream, stream.ContentType);
                    _mediaPlayer.PlaybackSession.PlaybackRate = RateToPlaybackSpeed(_rate);
                    _mediaPlayer.Volume = _volume / 100.0;
                    _mediaPlayer.Play();
                    return true;
                }
                else if (_sapiSynthesizer != null)
                {
                    // SAPI 5 (İngilizce Zira vb.) seslendirmesi
                    _sapiSynthesizer.Rate = _rate;
                    _sapiSynthesizer.Volume = _volume;
                    _sapiSynthesizer.SpeakAsync(text);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Status = SynthesisStatus.Idle;
                OnError?.Invoke($"Seslendirme hatası: {ex.Message}");
                return false;
            }

            return false;
        }

        public void Pause()
        {
            if (Status != SynthesisStatus.Speaking) return;

            try
            {
                if (_selectedVoice?.Engine == VoiceEngine.OneCore && _mediaPlayer != null)
                {
                    _mediaPlayer.Pause();
                    Status = SynthesisStatus.Paused;
                }
                else if (_sapiSynthesizer != null)
                {
                    _sapiSynthesizer.Pause();
                    Status = SynthesisStatus.Paused;
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Duraklatma hatası: {ex.Message}");
            }
        }

        public void Resume()
        {
            if (Status != SynthesisStatus.Paused) return;

            try
            {
                if (_selectedVoice?.Engine == VoiceEngine.OneCore && _mediaPlayer != null)
                {
                    _mediaPlayer.Play();
                    Status = SynthesisStatus.Speaking;
                }
                else if (_sapiSynthesizer != null)
                {
                    _sapiSynthesizer.Resume();
                    Status = SynthesisStatus.Speaking;
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Devam ettirme hatası: {ex.Message}");
            }
        }

        public void Stop()
        {
            try
            {
                if (_mediaPlayer != null)
                {
                    _mediaPlayer.Pause();
                    _mediaPlayer.Source = null;
                }

                if (_sapiSynthesizer != null)
                {
                    _sapiSynthesizer.SpeakAsyncCancelAll();
                }

                Status = SynthesisStatus.Idle;
            }
            catch { }
        }

        /// <summary>
        /// Metni seçili ses profili ile .wav ses dosyasına kaydeder.
        /// </summary>
        public async Task<bool> ExportToWavAsync(string text, string wavFilePath)
        {
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(wavFilePath)) return false;

            try
            {
                if (_selectedVoice?.Engine == VoiceEngine.OneCore && _oneCoreSynthesizer != null)
                {
                    using SpeechSynthesisStream stream = await _oneCoreSynthesizer.SynthesizeTextToStreamAsync(text);
                    using Stream netStream = stream.AsStreamForRead();
                    using FileStream fileStream = File.Create(wavFilePath);
                    await netStream.CopyToAsync(fileStream);
                    return true;
                }
                else if (_sapiSynthesizer != null)
                {
                    using var fileSynth = new System.Speech.Synthesis.SpeechSynthesizer();
                    fileSynth.Rate = _rate;
                    fileSynth.Volume = _volume;
                    if (_sapiSynthesizer.Voice != null && !string.IsNullOrEmpty(_sapiSynthesizer.Voice.Name))
                    {
                        try { fileSynth.SelectVoice(_sapiSynthesizer.Voice.Name); } catch { }
                    }

                    var audioFormat = new SpeechAudioFormatInfo(44100, AudioBitsPerSample.Sixteen, AudioChannel.Stereo);
                    fileSynth.SetOutputToWaveFile(wavFilePath, audioFormat);
                    fileSynth.Speak(text);
                    fileSynth.SetOutputToNull();
                    return true;
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"WAV dosyası oluşturulamadı: {ex.Message}");
                return false;
            }

            return false;
        }

        private static double RateToPlaybackSpeed(int rate)
        {
            // -10: 0.5x, 0: 1.0x, +10: 2.0x
            if (rate < 0)
                return 1.0 + (rate * 0.05); // 0 -> 1.0, -10 -> 0.5
            else
                return 1.0 + (rate * 0.1);  // 0 -> 1.0, +10 -> 2.0
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                Stop();

                _mediaPlayer?.Dispose();
                _mediaPlayer = null;

                _oneCoreSynthesizer?.Dispose();
                _oneCoreSynthesizer = null;

                _sapiSynthesizer?.Dispose();
                _sapiSynthesizer = null;
            }
        }
    }
}
