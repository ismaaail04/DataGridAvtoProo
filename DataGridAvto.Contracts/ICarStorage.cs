using DataGridAvto.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataGridAvto.Contracts
{
    /// <summary>
    /// Интерфейс хранилища данных авто
    /// </summary>
    public interface ICarStorage
    {
        /// <summary>
        /// Асинхронное получение всех данных
        /// </summary>
        Task<IReadOnlyCollection<Avto>> GetAllAsync();

        /// <summary>
        /// Асинхронное добавление
        /// </summary>
        Task<Avto> AddAsync(Avto Avto);

        /// <summary>
        /// Асинхронное изменение
        /// </summary>
        Task EditAsync(Avto Avto);

        /// <summary>
        /// Асинхронное удаление
        /// </summary>
        Task<bool> DeleteAsync(Guid id);
    }
}
