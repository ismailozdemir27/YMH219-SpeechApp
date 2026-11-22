using System;
using System.Collections.Generic;
using System.IO;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using SpeechApp.Enums;
using SpeechApp.Models;

namespace SpeechApp.Services
{
    /// <summary>
    /// Metinden Sese (Text-to-Speech / TTS) dönüşümünü yöneten,
    /// ses hızı, ses düzeyi, ses profili ve WAV dışa aktarımını destekleyen servis.
    /// </summary>
    public class TextToSpeechService : IDisposable
    {
        private SpeechSynthesizer? _synthesizer;
        private SynthesisStatus _status = SynthesisStatus.Idle;
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
            get => _synthesizer?.Rate ?? 0;
            set
            {
                if (_synthesizer != null)
                {
                    // Rate aralığı: -10 ile 10
                    int clamped = Math.Clamp(value, -10, 10);
                    _synthesizer.Rate = clamped;
                }
            }
        }

        public int Volume
        {
            get => _synthesizer?.Volume ?? 100;
            set
            {
                if (_synthesizer != null)
                {
                    // Volume aralığı: 0 ile 100
                    int clamped = Math.Clamp(value, 0, 100);
                    _synthesizer.Volume = clamped;
                }
            }
        }

        public event Action<SynthesisStatus>? OnStatusChanged;
        public event Action<int, int, string>? OnSpeakProgress; // charPosition, charCount, currentWord
        public event Action? OnSpeakCompleted;
        public event Action<string>? OnError;

        public TextToSpeechService()
        {
            InitializeSynthesizer();
        }

        private void InitializeSynthesizer()
        {
            try
            {
                _synthesizer = new SpeechSynthesizer();
                _synthesizer.SpeakProgress += Synthesizer_SpeakProgress;
                _synthesizer.SpeakCompleted += Synthesizer_SpeakCompleted;
                _synthesizer.StateChanged += Synthesizer_StateChanged;
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Konuşma sentezleyici başlatılamadı: {ex.Message}");
            }
        }

        private void Synthesizer_SpeakProgress(object? sender, SpeakProgressEventArgs e)
        {
            OnSpeakProgress?.Invoke(e.CharacterPosition, e.CharacterCount, e.Text ?? string.Empty);
        }

        private void Synthesizer_SpeakCompleted(object? sender, SpeakCompletedEventArgs e)
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
        }

        private void Synthesizer_StateChanged(object? sender, StateChangedEventArgs e)
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
        }

        /// <summary>
        /// İşletim sisteminde yüklü ses profillerini listeler.
        /// </summary>
        public List<VoiceModel> GetAvailableVoices()
        {
            var list = new List<VoiceModel>();
            if (_synthesizer == null) return list;

            try
            {
                foreach (InstalledVoice voice in _synthesizer.GetInstalledVoices())
                {
                    list.Add(new VoiceModel
                    {
                        Name = voice.VoiceInfo?.Name ?? "Bilinmeyen Ses",
                        Culture = voice.VoiceInfo?.Culture ?? System.Globalization.CultureInfo.InvariantCulture,
                        Gender = voice.VoiceInfo?.Gender.ToString() ?? "Belirtilmemiş",
                        Age = voice.VoiceInfo?.Age.ToString() ?? "Belirtilmemiş",
                        Enabled = voice.Enabled
                    });
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Ses profilleri taranırken hata: {ex.Message}");
            }

            return list;
        }

        /// <summary>
        /// Belirtilen ses profilini seçer.
        /// </summary>
        public bool SelectVoice(string voiceName)
        {
            if (_synthesizer == null) return false;

            try
            {
                _synthesizer.SelectVoice(voiceName);
                return true;
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Ses profili seçilemedi: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Verilen metni asenkron olarak seslendirir.
        /// </summary>
        public bool SpeakAsync(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return false;

            if (_synthesizer == null)
            {
                InitializeSynthesizer();
                if (_synthesizer == null) return false;
            }

            try
            {
                // Eğer zaten seslendiriliyorsa önce durdur
                if (Status == SynthesisStatus.Speaking || Status == SynthesisStatus.Paused)
                {
                    _synthesizer.SpeakAsyncCancelAll();
                }

                Status = SynthesisStatus.Speaking;
                _synthesizer.SpeakAsync(text);
                return true;
            }
            catch (Exception ex)
            {
                Status = SynthesisStatus.Idle;
                OnError?.Invoke($"Seslendirme başlatılırken hata: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Devam eden seslendirmeyi duraklatır.
        /// </summary>
        public void Pause()
        {
            if (_synthesizer != null && Status == SynthesisStatus.Speaking)
            {
                try
                {
                    _synthesizer.Pause();
                    Status = SynthesisStatus.Paused;
                }
                catch (Exception ex)
                {
                    OnError?.Invoke($"Duraklatma hatası: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Duraklatılmış seslendirmeye devam eder.
        /// </summary>
        public void Resume()
        {
            if (_synthesizer != null && Status == SynthesisStatus.Paused)
            {
                try
                {
                    _synthesizer.Resume();
                    Status = SynthesisStatus.Speaking;
                }
                catch (Exception ex)
                {
                    OnError?.Invoke($"Devam ettirme hatası: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Seslendirmeyi tamamen durdurur ve başa alır.
        /// </summary>
        public void Stop()
        {
            if (_synthesizer != null)
            {
                try
                {
                    _synthesizer.SpeakAsyncCancelAll();
                    Status = SynthesisStatus.Idle;
                }
                catch (Exception ex)
                {
                    OnError?.Invoke($"Durdurma hatası: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Verilen metni doğrudan bir .wav ses dosyasına kaydeder.
        /// </summary>
        public bool ExportToWav(string text, string wavFilePath)
        {
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(wavFilePath)) return false;

            try
            {
                using var fileSynth = new SpeechSynthesizer();
                // O anki seçili ses profilini, hız ve ses düzeyini uygula
                if (_synthesizer != null)
                {
                    fileSynth.Rate = _synthesizer.Rate;
                    fileSynth.Volume = _synthesizer.Volume;
                    try
                    {
                        if (_synthesizer.Voice != null && !string.IsNullOrEmpty(_synthesizer.Voice.Name))
                        {
                            fileSynth.SelectVoice(_synthesizer.Voice.Name);
                        }
                    }
                    catch { }
                }

                var audioFormat = new SpeechAudioFormatInfo(44100, AudioBitsPerSample.Sixteen, AudioChannel.Stereo);
                fileSynth.SetOutputToWaveFile(wavFilePath, audioFormat);
                fileSynth.Speak(text);
                fileSynth.SetOutputToNull();
                return true;
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"WAV dosyası oluşturulamadı: {ex.Message}");
                return false;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                if (_synthesizer != null)
                {
                    try
                    {
                        _synthesizer.SpeakAsyncCancelAll();
                        _synthesizer.Dispose();
                    }
                    catch { }
                    _synthesizer = null;
                }
            }
        }
    }
}
