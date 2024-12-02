using ConvertingSoundToColor.Models;
using ConvertingSoundToColor.Services;
using NAudio.Wave;
using System.Drawing;

namespace ConvertingSoundToColor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                // Проверяем наличие аргументов командной строки.
                string audioFilePath = string.Empty;
                if (args.Length < 1)
                {
                    Console.WriteLine("Пожалуйста, укажите путь к аудио файлу.");
                    audioFilePath = Console.ReadLine();
                }

                // Проверяем существование файла.
                if (!File.Exists(audioFilePath))
                {
                    Console.WriteLine("Указанный файл не существует.");
                    return;
                }

                AudioMoodAnalyzer analyzer = new AudioMoodAnalyzer();

                // Преобразуем аудио характеристики в цвет.
                RGBColor color = analyzer.AnalyzeMood(audioFilePath);

                // Создаем изображение.
                CreateImage(color, audioFilePath);

                // Выводим результат.
                Console.WriteLine($"Цвет на основе аудио файла: {color}");
                Console.WriteLine("\n\n\n");
                Console.ReadKey();
            }
        }

        private static AudioFeatures ExtractAudioFeatures(string audioFilePath)
        {
            using (var reader = new AudioFileReader(audioFilePath))
            {
                // Извлекаем продолжительность в секундах
                float durationSeconds = (float)reader.TotalTime.TotalSeconds;

                // Находим максимальную громкость
                float maxVolume = 0;
                float[] buffer = new float[reader.WaveFormat.SampleRate];
                int bytesRead;

                while ((bytesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int i = 0; i < bytesRead; i++)
                    {
                        // Вычисляем уровень громкости в каждом семпле.
                        float volume = Math.Abs(buffer[i]);
                        if (volume > maxVolume)
                        {
                            maxVolume = volume;
                        }
                    }
                }

                // Нормализуем громкость к диапазону 0-1.
                return new AudioFeatures
                {
                    MaxVolume = maxVolume,
                    DurationSeconds = (int)Math.Ceiling((double)durationSeconds)
                };
            }
        }

        private static void CreateImage(RGBColor color, string audioFilePath)
        {
            using (Bitmap bitmap = new Bitmap(720, 576))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.FromArgb(color.Red, color.Green, color.Blue));
                }

                // Получаем директорию файла и создаем полное имя для изображения
                string directory = Path.GetDirectoryName(audioFilePath);
                string fielName = audioFilePath.Split('\\').Last().Split('.').First();
                string imagePath = Path.Combine(directory, $"audio_color - {fielName}.jpg");

                // Сохраняем изображение в формате JPG
                bitmap.Save(imagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
    }
}
