using AutoMapper;
using MockQueryable.Moq;
using Moq;
using MotusInterview.Application.Common.Dto;
using MotusInterview.Application.Common.Interfaces;
using MotusInterview.Application.Vehicles.Queries.GetVehiclesWithPagination;
using MotusInterview.Domain.Entities;

namespace MotusInterview.Tests.Vehicles.Queries
{
    [TestFixture]
    public class GetVehiclesWithPaginationQueryHandlerTests
    {
        private Mock<IApplicationDbContext> _mockDbContext;
        private IMapper _mapper;
        private GetVehiclesWithPaginationQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockDbContext = new Mock<IApplicationDbContext>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new VehicleListItemDto.Mapping());  
            });
            _mapper = mapperConfig.CreateMapper();
            _handler = new GetVehiclesWithPaginationQueryHandler(_mockDbContext.Object, _mapper);
        }

        [Test]
        public async Task Handle_ShouldReturnPaginatedList_WhenVehiclesExist()
        {
            var vehicles = new List<Vehicle>
            {
                new Vehicle { VehicleId = 1, ManufacturerName = "Toyota", Model = "Corolla", ModelYear = 2020, Colour = new Colour { ColourId = ColourEnum.Red, ColourName = "Red", ColourHex = "#FF0000" } },
                new Vehicle { VehicleId = 2, ManufacturerName = "Honda", Model = "Civic", ModelYear = 2021, Colour = new Colour { ColourId = ColourEnum.Blue, ColourName = "Blue", ColourHex = "#0000FF" } },
                new Vehicle { VehicleId = 3, ManufacturerName = "Ford", Model = "Focus", ModelYear = 2019, Colour = new Colour { ColourId = ColourEnum.Green, ColourName = "Green", ColourHex = "#00FF00" } }
            }.AsQueryable();

            var mockVehicleDbSet = vehicles.BuildMockDbSet();
            _mockDbContext.Setup(db => db.Vehicles).Returns(mockVehicleDbSet.Object);
            var query = new GetVehiclesWithPaginationQuery { PageNumber = 1, PageSize = 3 };
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.That(result.TotalCount, Is.EqualTo(3)); 
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyList_WhenNoVehiclesExist()
        {
            var vehicles = new List<Vehicle>().AsQueryable();
            var mockVehicleDbSet = vehicles.BuildMockDbSet();
            _mockDbContext.Setup(db => db.Vehicles).Returns(mockVehicleDbSet.Object);
            var query = new GetVehiclesWithPaginationQuery { PageNumber = 1, PageSize = 10 };
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.That(result.TotalCount, Is.EqualTo(0));
        }
    }
}
