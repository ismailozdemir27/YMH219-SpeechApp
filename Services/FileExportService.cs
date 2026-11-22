using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SpeechApp.Models;

namespace SpeechApp.Services
{
    /// <summary>
    /// Metin ve konuşma kayıtlarını yerel dosya sistemine StreamWriter kullanarak
    /// UTF-8 formatında aktaran dosya dışa aktarım servisi.
    /// </summary>
    public class FileExportService
    {
        /// <summary>
        /// Düz metin içeriğini belirtilen yola UTF-8 olarak kaydeder.
        /// </summary>
        public bool SaveTextToFile(string filePath, string content)
        {
            try
            {
                using var writer = new StreamWriter(filePath, false, Encoding.UTF8);
                writer.Write(content);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Konuşma tanıma transkript ve geçmiş kayıtlarını detaylı rapor olarak kaydeder.
        /// </summary>
        public bool SaveTranscriptToFile(string filePath, IReadOnlyList<SpeechLogEntry> logs, string studentInfo = "İsmail Özdemir - 250541109")
        {
            try
            {
                using var writer = new StreamWriter(filePath, false, Encoding.UTF8);
                writer.WriteLine("================================================================================");
                writer.WriteLine("             YMH219 - SPEECH-TO-TEXT KONUŞMA TANIMA GÜNLÜK RAPORU              ");
                writer.WriteLine("================================================================================");
                writer.WriteLine($"Oluşturulma Tarihi : {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                writer.WriteLine($"Öğrenci Bilgisi    : {studentInfo}");
                writer.WriteLine($"Toplam Kayıt Sayısı: {logs.Count}");
                writer.WriteLine("--------------------------------------------------------------------------------");
                writer.WriteLine(string.Format("{0,-6} | {1,-12} | {2,-10} | {3,-10} | {4}", "NO", "ZAMAN", "KAYNAK", "GÜVEN", "ALGILANAN METİN"));
                writer.WriteLine("--------------------------------------------------------------------------------");

                int index = 1;
                foreach (var log in logs)
                {
                    writer.WriteLine(string.Format("{0,-6} | {1,-12} | {2,-10} | {3,-10} | {4}",
                        index++,
                        log.FormattedTimestamp,
                        log.Source,
                        log.ConfidencePercentage,
                        log.Text));
                }

                writer.WriteLine("================================================================================");
                writer.WriteLine("RAPOR SONU - YMH219 Nesne Tabanlı Programlama");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
