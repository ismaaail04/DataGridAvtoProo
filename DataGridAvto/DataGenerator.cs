using DataGridAvto.Contracts.Models;
using System;


namespace DataGridAvto
{
    internal class DataGenerator
    {
        public static Avto CreateAvto(Action<Avto> settings = null)
        {
            var result = new Avto
            {
                Id = Guid.NewGuid(),
            };

            settings?.Invoke(result);

            return result;
        }
    }
}
