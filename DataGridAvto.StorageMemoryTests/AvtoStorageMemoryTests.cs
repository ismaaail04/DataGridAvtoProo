using DataGridAvto.Contracts;
using DataGridAvto.Contracts.Models;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DataGridAvto.StorageMemory.Tests
{
    /// <summary>
    /// Тесты для класса <see cref="MemoryCarStorage"/>
    /// </summary>
    public class AvtoStorageMemoryTests
    {
        private readonly ICarStorage carStorage;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AvtoStorageMemoryTests"/>
        /// </summary>
        public AvtoStorageMemoryTests()
        {
            carStorage = new MemoryCarStorage();
        }

        /// <summary>
        /// При пустом списке нет ошибок <see cref="ICarStorage.GetAllAsync"/>
        /// </summary>
        [Fact]
        public async Task GetAllShouldNotThrow()
        {
            // Act
            Func<Task> act = () => carStorage.GetAllAsync();

            // Assert
            await act.Should().NotThrowAsync();
        }

        /// <summary>
        /// Получает пустой список <see cref="ICarStorage.GetAllAsync"/>
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await carStorage.GetAllAsync();

            // Assert
            result.Should()
            .NotBeNull()
            .And.BeEmpty();
        }

        /// <summary>
        /// Тест: Метод <see cref="ICarStorage.AddAsync"/> должен вернуть добавленный объект.
        /// </summary>
        [Fact]
        public async Task AddShouldReturnValue()
        {
            // Arrange
            var model = new Avto
            {
                Id = Guid.NewGuid(),
                Mark = Mark.Hunday_Creta,
                Number = "e332ew321",
                Probeg = 111,
                AvgFuelCons = 2,
                CurrFuel = 5,
                CostRent = 33,
            };

            // Act
            var result = await carStorage.AddAsync(model);

            // Assert
            result.Should().NotBeNull()
                .And.BeEquivalentTo(new
                {
                    model.Id,
                    model.Mark,
                });
        }

        /// <summary>
        /// Удаление из хранилища
        /// </summary>
        [Fact]
        public async Task DeleteShouldReturnTrue()
        {
            // Arrange
            var model = new Avto
            {
                Id = Guid.NewGuid(),
                Mark = Mark.Hunday_Creta,
                Number = "e332ew321",
                Probeg = 111,
                AvgFuelCons = 2,
                CurrFuel = 5,
                CostRent = 33,
            };

            // Act
            await carStorage.AddAsync(model);
            var result = await carStorage.DeleteAsync(model.Id);

            var nailList = await carStorage.GetAllAsync();
            var tryGetModel = nailList.FirstOrDefault(x => x.Id == model.Id);

            // Assert
            result.Should().BeTrue();
            tryGetModel.Should().BeNull();
        }

        /// <summary>
        /// Изменение данных в хранилище
        /// </summary>
        [Fact]
        public async Task EditShouldUpdateStorageData()
        {
            // Arrange
            var model = new Avto
            {
                Id = Guid.NewGuid(),
                Mark = Mark.Hunday_Creta,
                Number = "e332ew321",
                Probeg = 111,
                AvgFuelCons = 2,
                CurrFuel = 5,
                CostRent = 33,
            };

            // Act
            await carStorage.AddAsync(model);
            await carStorage.EditAsync(model);
            var applicantList = await carStorage.GetAllAsync();
            var result = applicantList.FirstOrDefault(x => x.Id == model.Id);

            // Assert
            result?.Should().NotBeNull();
            result?.Id.Should().Be(model.Id);
            result?.Mark.Should().Be(Mark.Hunday_Creta);
        }
    }
}
