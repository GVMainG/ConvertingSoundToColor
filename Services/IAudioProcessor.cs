using ConvertingSoundToColor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertingSoundToColor.Services
{
    /// <summary>
    /// Интерфейс для обработки аудио файлов и извлечения их характеристик.
    /// </summary>
    internal interface IAudioProcessor
    {
        /// <summary>
        /// Извлекает основные характеристики из аудио файла.
        /// </summary>
        /// <param name="filePath">Путь к аудио файлу.</param>
        /// <returns>Определённые характеристики аудио файла.</returns>
        AudioFeatures ExtractFeatures(string filePath);
    }
}
