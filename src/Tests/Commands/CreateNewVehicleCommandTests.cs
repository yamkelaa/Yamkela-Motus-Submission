using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using MotusInterview.Application.Vehicles.Commands.CreateVehicle;
using MotusInterview.Application.Common.Interfaces;
using MotusInterview.Application.Common.Models;
using MotusInterview.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MotusInterview.Tests.Vehicles.Commands
{
    [TestFixture]
    public class CreateNewVehicleCommandHandlerTests
    {
        private Mock<IApplicationDbContext> _mockDbContext;
        private CreateNewVehicleQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockDbContext = new Mock<IApplicationDbContext>();
            var mockVehicleDbSet = new Mock<DbSet<Vehicle>>();
            _mockDbContext.Setup(db => db.Vehicles).Returns(mockVehicleDbSet.Object);
            _handler = new CreateNewVehicleQueryHandler(_mockDbContext.Object);
        }

        [Test]
        public async Task Handle_ShouldCreateNewVehicle_WhenValidDataIsProvided()
        {
            var vehicleForm = new VehicleForm
            {
                ManufacturerName = "Toyota",
                Model = "Camry",
                ModelYear = 2023,
                ColourId = ColourEnum.Black
            };

            var command = new CreateNewVehicleCommand
            {
                VehicleForm = vehicleForm
            };

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.Succeeded);

            _mockDbContext.Verify(db => db.Vehicles.Add(It.Is<Vehicle>(v =>
                v.ManufacturerName == vehicleForm.ManufacturerName &&
                v.Model == vehicleForm.Model &&
                v.ModelYear == vehicleForm.ModelYear &&
                v.ColourId == vehicleForm.ColourId)), Times.Once);

            _mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
