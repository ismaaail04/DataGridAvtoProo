using DataGridAvto.Contracts;
using DataGridAvto.Contracts.Models;
using DataGridAvto.AvtoManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace DataGridAvto.AvtoManager
{
    /// <inheritdoc cref="CarManager"/>
    public class CarManager : ICarManager
    {
        private readonly ICarStorage carStorage;
        private readonly ILogger logger;

        /// <summary>
        /// Конструктор
        /// </summary
        public CarManager(ICarStorage carStorage, ILogger logger)
        {
            this.logger = logger;
            this.carStorage = carStorage;
        }

        /// <intheridoc cref="ICarManager.AddAsync(Avto)"/>
        public async Task<Avto> AddAsync(Avto avto)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Avto result;
            try
            {
                result = await carStorage.AddAsync(avto);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                LoggingHelper.LogErrorAvto(
                    logger,
                    nameof(ICarManager.AddAsync),
                    avto.Id,
                    stopwatch.ElapsedMilliseconds,
                    ex.Message,
                    avto.Number
                    );
                return null;
            }
            stopwatch.Stop();
            LoggingHelper.LogInfoCar(
                logger,
                nameof(ICarManager.AddAsync),
                avto.Id,
                stopwatch.ElapsedMilliseconds,
                avto.Number
                );
            return result;
        }

        /// <inheritdoc cref="ICarManager.DeleteAsync(Guid)"/>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            bool result;

            try
            {
                result = await carStorage.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                LoggingHelper.LogErrorAvto(logger, nameof(ICarManager.DeleteAsync),
                         id,
                         stopwatch.ElapsedMilliseconds,
                         ex.Message
                         );
                return false;
            }

            stopwatch.Stop();
            LoggingHelper.LogInfoCar(logger, nameof(ICarManager.DeleteAsync),
                    id,  
                    stopwatch.ElapsedMilliseconds
                );
            return result;
        }

        /// <inheritdoc cref="ICarManager.EditAsync(Avto)"/>
        public async Task EditAsync(Avto avto)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                await carStorage.EditAsync(avto);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                LoggingHelper.LogErrorAvto(logger, nameof(ICarManager.EditAsync),
                         avto.Id,
                         stopwatch.ElapsedMilliseconds,
                         ex.Message,
                         avto.Number
                         );
            }

            stopwatch.Stop();
            LoggingHelper.LogInfoCar(logger, nameof(ICarManager.EditAsync),
                    avto.Id,
                    stopwatch.ElapsedMilliseconds,
                    avto.Number
                );
        }

        /// <inheritdoc cref="ICarManager.GetAllAsync()"/>
        public async Task<IReadOnlyCollection<Avto>> GetAllAsync()
        {
            try
            {
                return await carStorage.GetAllAsync();
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(logger, nameof(ICarManager.GetAllAsync), ex.Message);
            }
            return null;
        }

        /// <inheritdoc cref="ICarManager.GetAllAsync()"/>
        public async Task<ICarStats> GetStatsAsync()
        {
            try
            {
                var items = await carStorage.GetAllAsync();
                int lowCount = items.Count(i => i.CurrFuel < 7);
                return new CarStatsModel
                {
                    Count = items.Count,
                    LowCount = lowCount
                };
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(logger, nameof(ICarManager.GetStatsAsync), ex.Message);
            }
            return null;
        }
    }
}
