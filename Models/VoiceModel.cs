using System.Globalization;

namespace SpeechApp.Models
{
    /// <summary>
    /// İşletim sisteminde yüklü ses profillerini temsil eden model sınıfı.
    /// </summary>
    public class VoiceModel
    {
        public string Name { get; set; } = string.Empty;
        public CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;
        public string Gender { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;

        public string DisplayName => $"{Name} ({Culture.DisplayName} - {Gender})";

        public override string ToString() => DisplayName;
    }
}
