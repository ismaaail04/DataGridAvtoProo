using DataGridAvto.Contracts;
using DataGridAvto.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGridAvto.StorageMemory
{
    /// <inheritdoc cref="ICarStorage"/>
    public class MemoryCarStorage : ICarStorage
    {
        private List<Avto> car;

        public MemoryCarStorage()
        {
            car = new List<Avto>();
        }

        /// <summary>
        /// Асинхронный метод добавления авто
        /// </summary>
        public Task<Avto> AddAsync(Avto avto)
        {
            car.Add(avto);
            return Task.FromResult(avto);
        }

        /// <summary>
        /// Асинхронный метод удаления авто
        /// </summary>
        public Task<bool> DeleteAsync(Guid id)
        {
            var avto = car.FirstOrDefault(x => x.Id == id);
            if (avto != null)
            {
                car.Remove(avto);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        /// <summary>
        /// Асинхронный метод редактирования авто
        /// </summary>
        public Task EditAsync(Avto avto)
        {
            var target = car.FirstOrDefault(x => x.Id == avto.Id);
            if (avto != null)
            {
                target.Mark = avto.Mark;
                target.Number = avto.Number;
                target.Probeg = avto.Probeg;
                target.AvgFuelCons = avto.AvgFuelCons;
                target.CurrFuel = avto.CurrFuel;
                target.CostRent = avto.CostRent;
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Получение авто из хранилища
        /// </summary>
        public Task<IReadOnlyCollection<Avto>> GetAllAsync()
            => Task.FromResult<IReadOnlyCollection<Avto>>(car);
    }
}
