namespace ConvertingSoundToColor.Models
{
    /// <summary>
    /// Модель для представления цвета в формате RGB.
    /// </summary>
    internal class RGBColor
    {
        /// <summary>
        /// Красный компонент цвета.
        /// </summary>
        public int Red { get; }

        /// <summary>
        /// Зеленый компонент цвета.
        /// </summary>
        public int Green { get; }

        /// <summary>
        /// Синий компонент цвета.
        /// </summary>
        public int Blue { get; }

        /// <summary>
        /// Создаёт экземпляр RGB цветового объекта.
        /// </summary>
        /// <param name="red">Красный компонент.</param>
        /// <param name="green">Зеленый компонент.</param>
        /// <param name="blue">Синий компонент.</param>
        public RGBColor(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        /// <summary>
        /// Возвращает строковое представление цвета в формате RGB.
        /// </summary>
        /// <returns>Цвет в формате строки.</returns>
        public override string ToString()
        {
            return $"RGB({Red}, {Green}, {Blue})";
        }
    }
}
