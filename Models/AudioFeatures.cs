namespace ConvertingSoundToColor.Models
{
    /// <summary>
    /// Модель для хранения характеристик аудио файла.
    /// </summary>
    internal class AudioFeatures
    {
        /// <summary>
        /// Максимальная громкость в аудио файле.
        /// </summary>
        public float MaxVolume { get; set; }

        /// <summary>
        /// Длительность аудио файла в секундах.
        /// </summary>
        public int DurationSeconds { get; set; }
    }
}
