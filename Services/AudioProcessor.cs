using ConvertingSoundToColor.Models;
using NAudio.Wave;

namespace ConvertingSoundToColor.Services
{
    /// <summary>
    /// Класс для обработки аудио файлов и извлечения их характеристик.
    /// </summary>
    internal class AudioProcessor : IAudioProcessor
    {
        /// <inheritdoc/>
        public AudioFeatures ExtractFeatures(string filePath)
        {
            // Проверка существования файла
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Аудио файл не найден.", filePath);
            }

            // Используем NAudio для обработки файла
            using var audioFileReader = new AudioFileReader(filePath);
            float maxVolume = 0;

            // Перебираем данные аудио, чтобы получить характеристики
            while (audioFileReader.Position < audioFileReader.Length)
            {
                float[] buffer = new float[1024];
                int bytesRead = audioFileReader.Read(buffer, 0, buffer.Length);

                // Определяем максимальный уровень громкости
                for (int n = 0; n < bytesRead; n++)
                {
                    float absValue = Math.Abs(buffer[n]);
                    if (absValue > maxVolume) maxVolume = absValue;
                }
            }

            // Возвращаем основные извлечённые характеристики
            return new AudioFeatures
            {
                MaxVolume = maxVolume,
                DurationSeconds = (int)audioFileReader.TotalTime.TotalSeconds
            };
        }
    }
}
