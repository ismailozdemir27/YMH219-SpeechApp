namespace SpeechApp.Enums
{
    /// <summary>
    /// Metinden Sese (Text-to-Speech) motorunun çalışma durumları.
    /// </summary>
    public enum SynthesisStatus
    {
        Idle,       // Boşta / Hazır
        Speaking,   // Seslendiriliyor
        Paused      // Duraklatıldı
    }
}
