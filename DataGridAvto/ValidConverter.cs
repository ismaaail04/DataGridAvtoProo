using DataGridAvto.Contracts.Models;

namespace DataGridAvto
{
    /// <summary>
    /// Методы приведения одного типа к другому
    /// </summary>
    public static class ValidConverter
    {
        /// <summary>
        /// Привести <see cref="Avto"/> к <see cref="ValidAvto"/>
        /// </summary>
        public static Avto ToValidAvto(ValidAvto validAvto)
        {
            return new Avto()
            {
                Id = validAvto.Id,
                Mark = validAvto.Mark,
                Number = validAvto.Number,
                Probeg = validAvto.Probeg,
                AvgFuelCons = validAvto.AvgFuelCons,
                CurrFuel = validAvto.CurrFuel,
                CostRent = validAvto.CostRent,
            };
        }

        /// <summary>
        /// Привести <see cref="ValidAvto"/> к <see cref="Avto"/>
        /// </summary>
        public static ValidAvto ToAvto(Avto avto)
        {
            return new ValidAvto()
            {
                Id = avto.Id,
                Mark = avto.Mark,
                Number = avto.Number,
                Probeg = avto.Probeg,
                AvgFuelCons = avto.AvgFuelCons,
                CurrFuel = avto.CurrFuel,
                CostRent = avto.CostRent,
            };
        }
    }
}
