using DataGridAvto.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataGridAvto.Contracts
{
    public interface ICarManager
    {
        /// <summary>
        /// Асинхронное получение всех данных
        /// </summary
        Task<IReadOnlyCollection<Avto>> GetAllAsync();

        /// <summary>
        /// Асинхронное добавление
        /// </summary>
        Task<Avto> AddAsync(Avto avto);

        /// <summary>
        /// Асинхронное изменение
        /// </summary>
        Task EditAsync(Avto avto);

        /// <summary>
        /// Асинхронное удаление
        /// </summary>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Асинхронное получение суммарных данных
        /// </summary>
        Task<ICarStats> GetStatsAsync();

    }
}
