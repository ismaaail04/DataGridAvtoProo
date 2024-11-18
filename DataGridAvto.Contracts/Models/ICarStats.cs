namespace DataGridAvto.Contracts.Models
{
    /// <summary>
    /// Интерфейс для вычисляемых данных о списке <see cref="Avto"/>
    /// </summary>
    public interface ICarStats
    {
        /// <summary>
        /// Общее количество авто
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Количество авто с критически низким уровенем запаса хода
        /// </summary>
        int LowCount { get; }
    }
}
