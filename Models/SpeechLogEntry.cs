using System;

namespace SpeechApp.Models
{
    /// <summary>
    /// Algılanan veya seslendirilen konuşma olayını tutan model sınıfı.
    /// </summary>
    public class SpeechLogEntry
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Text { get; set; } = string.Empty;
        public float Confidence { get; set; } = 1.0f; // 0.0 - 1.0 arası güven puanı
        public string Source { get; set; } = "Mikrofon"; // "Mikrofon" veya "Simülasyon"

        public string ConfidencePercentage => $"{Math.Round(Confidence * 100, 1)}%";

        public string FormattedTimestamp => Timestamp.ToString("HH:mm:ss.fff");

        public override string ToString()
        {
            return $"[{FormattedTimestamp}] [{Source}] (Güven: {ConfidencePercentage}) \"{Text}\"";
        }
    }
}
