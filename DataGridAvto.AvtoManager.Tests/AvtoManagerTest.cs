using DataGridAvto.Contracts;
using DataGridAvto.Contracts.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DataGridAvto.AvtoManager.Tests
{
    /// <summary>
    /// Тесты для класса <see cref="CarManager"/>.
    /// </summary>
    public class AvtoManagerTest
    {
        private readonly ICarManager carManager;
        private readonly Mock<ICarStorage> StorageMock;
        private readonly Mock<ILogger> loggerMock;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CarManager"/>.
        /// </summary>
        public AvtoManagerTest()
        {
            StorageMock = new Mock<ICarStorage>();
            loggerMock = new Mock<ILogger>();

            loggerMock.Setup(x => x.Log(LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()));

            carManager = new CarManager(StorageMock.Object, loggerMock.Object);
        }

        /// <summary>
        /// Тест: Метод <see cref="CarManager.AddAsync"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
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
            StorageMock.Setup(x => x.AddAsync(It.IsAny<Avto>()))
                .ReturnsAsync(model);

            // Act
            var result = await carManager.AddAsync(model);

            // Asset
            result.Should().NotBeNull()
                .And.Be(model);

            loggerMock.Verify(x => x.Log
            (LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((state, t) => state.ToString().Contains(nameof(ICarManager.AddAsync))),
            null,
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
            loggerMock.VerifyNoOtherCalls();

            StorageMock.Verify(x => x.AddAsync(It.Is<Avto>(y => y.Id == model.Id)),
                Times.Once);
            StorageMock.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Тест: Метод <see cref="CarManager.EditAsync"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
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
            StorageMock.Setup(x => x.EditAsync(It.IsAny<Avto>())).Returns(Task.CompletedTask);

            // Act
            await carManager.EditAsync(model);

            // Asset
            loggerMock.Verify(x => x.Log
            (LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((state, t) => state.ToString().Contains(nameof(ICarManager.EditAsync))),
            null,
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
            loggerMock.VerifyNoOtherCalls();

            StorageMock.Verify(x => x.EditAsync(It.Is<Avto>(y => y.Id == model.Id)),
                Times.Once);
            StorageMock.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Тест: Метод <see cref="CarManager.DeleteAsync"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
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
            StorageMock.Setup(x => x.DeleteAsync(model.Id))
                .ReturnsAsync(true);

            // Act
            var result = await carManager.DeleteAsync(model.Id);

            // Asset
            result.Should().BeTrue();

            loggerMock.Verify(x => x.Log
            (LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((state, t) => state.ToString().Contains(nameof(ICarManager.DeleteAsync))),
            null,
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
            loggerMock.VerifyNoOtherCalls();

            StorageMock.Verify(x => x.DeleteAsync(model.Id),
                Times.Once);
            StorageMock.VerifyNoOtherCalls();

        }
    }
}
