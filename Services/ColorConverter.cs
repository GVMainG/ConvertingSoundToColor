using ConvertingSoundToColor.Models;

namespace ConvertingSoundToColor.Services
{
    /// <summary>
    /// Класс, преобразующий аудио характеристики в цвет RGB.
    /// </summary>
    internal class ColorConverter : IColorConverter
    {
        /// <inheritdoc/>
        public RGBColor ConvertToColor(AudioFeatures features)
        {
            // Придуманный принцип преобразования:
            // Используем максимальную громкость для определения "интенсивности" цвета
            // и длительность для смешивания в других компонентах RGB.

            // Здесь maxVolume и duration в конкретном диапазоне [0,1]
            float red = features.MaxVolume * 255; // Громкость влияет на яркость красного
            float green = features.DurationSeconds % 256; // Остаток от деления влияет на зеленый
            float blue = (255 - features.MaxVolume * 255); // Инвертированный громкость влияет на синий

            // Ограничиваем значения в рамках от 0 до 255
            red = Math.Clamp(red, 0, 255);
            green = Math.Clamp(green, 0, 255);
            blue = Math.Clamp(blue, 0, 255);

            return new RGBColor((int)red, (int)green, (int)blue);
        }
    }
}
