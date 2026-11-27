namespace SpeechApp.Enums
{
    /// <summary>
    /// Sesten Metne (Speech-to-Text) dinleme motorunun durumları.
    /// </summary>
    public enum RecognitionStatus
    {
        Idle,           // Dinleme kapalı
        Listening,      // Mikrofon açık, ses dinleniyor
        Processing,     // Konuşma işleniyor / algılandı
        Unavailable     // Mikrofon veya konuşma motoru bulunamadı / hata
    }
}
