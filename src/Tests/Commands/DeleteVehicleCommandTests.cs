using MockQueryable.Moq;
using Moq;
using MotusInterview.Application.Common.Interfaces;
using MotusInterview.Application.Vehicles.Commands.DeleteVehicle;
using MotusInterview.Domain.Entities;

namespace MotusInterview.Tests.Vehicles.Commands
{
    [TestFixture]
    public class DeleteVehicleCommandHandlerTests
    {
        private Mock<IApplicationDbContext> _mockDbContext;
        private DeleteVehicleByIdCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockDbContext = new Mock<IApplicationDbContext>();
            _handler = new DeleteVehicleByIdCommandHandler(_mockDbContext.Object);
        }

        [Test]
        public async Task DeleteVehicle_ShouldReturnSuccess_WhenVehicleExists()
        {
            var vehicle = new Vehicle
            {
                VehicleId = 1,
                ManufacturerName = "Toyota",
                Model = "Camry",
                ModelYear = 2023,
                ColourId = ColourEnum.Black
            };

            var vehicles = new List<Vehicle> { vehicle }.AsQueryable();
            var mockVehicleDbSet = vehicles.BuildMockDbSet();
            _mockDbContext.Setup(db => db.Vehicles).Returns(mockVehicleDbSet.Object);
            var command = new DeleteVehicleCommand { VehicleId = 1 };
            var result = await _handler.Handle(command, CancellationToken.None);
            Assert.IsTrue(result.Succeeded);
            _mockDbContext.Verify(db => db.Vehicles.Remove(It.Is<Vehicle>(v => v.VehicleId == 1)), Times.Once);
            _mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task DeleteVehicle_ShouldReturnFailure_WhenVehicleNotFound()
        {
            var vehicles = new List<Vehicle>().AsQueryable();
            var mockVehicleDbSet = vehicles.BuildMockDbSet();
            _mockDbContext.Setup(db => db.Vehicles).Returns(mockVehicleDbSet.Object);

            
            var command = new DeleteVehicleCommand { VehicleId = 1 };
            var result = await _handler.Handle(command, CancellationToken.None);

            
            Assert.IsFalse(result.Succeeded);
            _mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
