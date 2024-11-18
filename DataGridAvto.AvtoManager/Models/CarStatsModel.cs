using DataGridAvto.Contracts.Models;

namespace DataGridAvto.AvtoManager.Models
{
    public class CarStatsModel : ICarStats
    {
        public int Count { get; set; }

        public int LowCount { get; set; }
    }
}
