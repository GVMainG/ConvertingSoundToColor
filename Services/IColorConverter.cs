using ConvertingSoundToColor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertingSoundToColor.Services
{
    /// <summary>
    /// Интерфейс для преобразования аудио характеристик в цветовые данные.
    /// </summary>
    internal interface IColorConverter
    {
        /// <summary>
        /// Преобразует характеристики аудио файла в RGB цвет.
        /// </summary>
        /// <param name="features">Характеристики аудио файла.</param>
        /// <returns>Цвет в RGB.</returns>
        RGBColor ConvertToColor(AudioFeatures features);
    }
}
