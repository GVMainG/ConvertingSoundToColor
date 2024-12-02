using ConvertingSoundToColor.Models;
using NAudio.Wave;
using System.Numerics;

namespace ConvertingSoundToColor.Services
{
    internal class AudioMoodAnalyzer : IAudioMoodAnalyzer
    {
        public RGBColor AnalyzeMood(string filePath)
        {
            // Открываем аудио файл для анализа
            using (var audioFileReader = new AudioFileReader(filePath))
            {
                // Буфер для извлечения аудио данных в формате float
                float[] buffer = new float[audioFileReader.WaveFormat.SampleRate];
                int bytesRead;

                // Переменные для накопления энергии в различных частотных диапазонах
                double lowFrequencyEnergy = 0;
                double midFrequencyEnergy = 0;
                double highFrequencyEnergy = 0;

                // Цикл обработки каждой порции данных
                while ((bytesRead = audioFileReader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    // Преобразуем временные данные в частотные через FFT
                    Complex[] complexBuffer = new Complex[bytesRead];
                    for (int i = 0; i < bytesRead; i++)
                    {
                        complexBuffer[i] = new Complex(buffer[i], 0);
                    }

                    // Применяем Фурье-преобразование
                    FFT(complexBuffer);

                    // Анализируем спектр
                    for (int i = 0; i < complexBuffer.Length / 2; i++)
                    {
                        double magnitude = complexBuffer[i].Magnitude;

                        // Простой фильтр частот
                        if (i < 200) // Низкие частоты (до ~1.5 кГц)
                        {
                            lowFrequencyEnergy += magnitude;
                        }
                        else if (i < 2000) // Средние частоты
                        {
                            midFrequencyEnergy += magnitude;
                        }
                        else // Высокие частоты (выше ~1.5 кГц)
                        {
                            highFrequencyEnergy += magnitude;
                        }
                    }
                }

                // Вычисляем долю каждой категории
                double totalEnergy = lowFrequencyEnergy + midFrequencyEnergy + highFrequencyEnergy;
                double redWeight = highFrequencyEnergy / totalEnergy;
                double greenWeight = midFrequencyEnergy / totalEnergy;
                double blueWeight = lowFrequencyEnergy / totalEnergy;

                // Преобразуем в RGB, масштабируем от 0 до 255
                int red = (int)(255 * redWeight);
                int green = (int)(255 * greenWeight);
                int blue = (int)(255 * blueWeight);

                return new RGBColor(red, green, blue);
            }
        }

        private void FFT(Complex[] buffer)
        {
            int n = buffer.Length;

            if (n == 1)
                return;

            // Дерево бабочки
            int halfSize = n / 2;
            Complex[] even = new Complex[halfSize];
            Complex[] odd = new Complex[halfSize];

            for (int i = 0; i < halfSize; i++)
            {
                even[i] = buffer[i * 2];
                odd[i] = buffer[i * 2 + 1];
            }

            FFT(even);
            FFT(odd);

            for (int i = 0; i < halfSize; i++)
            {
                double angle = -2 * Math.PI * i / n;
                Complex twiddleFactor = new Complex(Math.Cos(angle), Math.Sin(angle)) * odd[i];
                buffer[i] = even[i] + twiddleFactor;
                buffer[i + halfSize] = even[i] - twiddleFactor;
            }
        }
    }
}
