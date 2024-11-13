using MockQueryable.Moq;
using Moq;
using MotusInterview.Application.Common.Interfaces;
using MotusInterview.Application.Common.Models;
using MotusInterview.Application.Vehicles.Commands.UpdateVehicle;
using MotusInterview.Domain.Entities;

namespace MotusInterview.Tests.Vehicles.Commands
{
    [TestFixture]
    public class UpdateVehicleCommandHandlerTests
    {
        private Mock<IApplicationDbContext> _mockDbContext;
        private UpdateVehicleCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockDbContext = new Mock<IApplicationDbContext>();
            _handler = new UpdateVehicleCommandHandler(_mockDbContext.Object);
        }

        [Test]
        public async Task Handle_ShouldUpdateVehicle_WhenVehicleExists()
        {
            var existingVehicle = new Vehicle
            {
                VehicleId = 1,
                ManufacturerName = "Toyota",
                Model = "Corolla",
                ModelYear = 2020,
                ColourId = ColourEnum.Black
            };

            var vehicles = new List<Vehicle> { existingVehicle }.AsQueryable();
            var mockVehicleDbSet = vehicles.BuildMockDbSet();
            _mockDbContext.Setup(db => db.Vehicles).Returns(mockVehicleDbSet.Object);

            var updateVehicleForm = new VehicleForm
            {
                ManufacturerName = "Toyota",
                Model = "Camry",
                ModelYear = 2023,
                ColourId = ColourEnum.Red
            };

            var command = new UpdateVehicleCommand
            {
                VehicleId = 1,
                VehicleForm = updateVehicleForm
            };
            var result = await _handler.Handle(command, CancellationToken.None);
            Assert.IsTrue(result.Succeeded);
            _mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenVehicleDoesNotExist()
        {
            var vehicles = new List<Vehicle>().AsQueryable(); 
            var mockVehicleDbSet = vehicles.BuildMockDbSet();
            _mockDbContext.Setup(db => db.Vehicles).Returns(mockVehicleDbSet.Object);

            var updateVehicleForm = new VehicleForm
            {
                ManufacturerName = "Toyota",
                Model = "Camry",
                ModelYear = 2023,
                ColourId = ColourEnum.Red
            };

            var command = new UpdateVehicleCommand
            {
                VehicleId = 1,
                VehicleForm = updateVehicleForm
            };
            var result = await _handler.Handle(command, CancellationToken.None);
            Assert.IsFalse(result.Succeeded);
            _mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
