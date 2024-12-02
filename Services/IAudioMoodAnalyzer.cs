using ConvertingSoundToColor.Models;

namespace ConvertingSoundToColor.Services
{
    internal interface IAudioMoodAnalyzer
    {
        /// <summary>
        /// Анализирует аудио файл для определения его настроения и преобразует это в цвет.
        /// </summary>
        /// <param name="filePath">Путь к аудио файлу.</param>
        /// <returns>Цвет, основанный на настроении аудио файла.</returns>
        RGBColor AnalyzeMood(string filePath);
    }
}
