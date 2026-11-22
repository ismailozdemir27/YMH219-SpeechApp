using System.Globalization;

namespace SpeechApp.Models
{
    public enum VoiceEngine
    {
        Sapi5,      // System.Speech (Klasik Masaüstü)
        OneCore     // Windows.Media.SpeechSynthesis (Modern Windows / Türkçe Tolga)
    }

    /// <summary>
    /// İşletim sisteminde yüklü ses profillerini temsil eden model sınıfı.
    /// Hem klasik SAPI 5 hem de modern OneCore (Türkçe Tolga) seslerini destekler.
    /// </summary>
    public class VoiceModel
    {
        public string Name { get; set; } = string.Empty;
        public CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;
        public string Gender { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;
        public VoiceEngine Engine { get; set; } = VoiceEngine.Sapi5;
        public string VoiceId { get; set; } = string.Empty;

        public string FlagEmoji => Culture.TwoLetterISOLanguageName.ToLower() switch
        {
            "tr" => "🇹🇷",
            "en" => "🇺🇸",
            "de" => "🇩🇪",
            "fr" => "🇫🇷",
            "es" => "🇪🇸",
            "it" => "🇮🇹",
            _ => "🌐"
        };

        public string EngineLabel => Engine == VoiceEngine.OneCore ? "OneCore" : "SAPI 5";

        public string DisplayName => $"{FlagEmoji} {Name} ({Culture.DisplayName} - {Gender}) [{EngineLabel}]";

        public override string ToString() => DisplayName;
    }
}
