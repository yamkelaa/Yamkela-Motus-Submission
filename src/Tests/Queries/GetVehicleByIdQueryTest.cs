using AutoMapper;
using MockQueryable.Moq;
using Moq;
using MotusInterview.Application.Common.Dto.Request;
using MotusInterview.Application.Common.Interfaces;
using MotusInterview.Application.Vehicles.Queries.GetVehicleById;
using MotusInterview.Domain.Entities;

[TestFixture]
public class GetVehicleByIdQueryHandlerTests
{
    private Mock<IApplicationDbContext> _mockDbContext;
    private IMapper _mapper;
    private GetVehicleByIdQueryHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockDbContext = new Mock<IApplicationDbContext>();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Vehicle, VehiclesDto>();      
            cfg.CreateMap<Colour, ColoursDto>();       
        });
        _mapper = mapperConfig.CreateMapper();
        _handler = new GetVehicleByIdQueryHandler(_mockDbContext.Object, _mapper);
    }

    [Test]
    public async Task Handle_ShouldReturnVehicle_WhenVehicleExists()
    {
        var vehicles = new List<Vehicle>
        {
            new Vehicle
            {
                VehicleId = 1,
                ManufacturerName = "Toyota",
                Model = "Corolla",
                ModelYear = 2020,
                ColourId = ColourEnum.Red,
                Colour = new Colour { ColourId = ColourEnum.Red, ColourName = "Red", ColourHex = "#FF0000" }
            },
            new Vehicle
            {
                VehicleId = 2,
                ManufacturerName = "Honda",
                Model = "Civic",
                ModelYear = 2021,
                ColourId = ColourEnum.Blue,
                Colour = new Colour { ColourId = ColourEnum.Blue, ColourName = "Blue", ColourHex = "#0000FF" }
            }
        }.AsQueryable();
        var mockVehicleDbSet = vehicles.BuildMockDbSet();
        _mockDbContext.Setup(db => db.Vehicles).Returns(mockVehicleDbSet.Object);
        var query = new GetVehicleByIdQuery { VehicleId = 2 };
        var result = await _handler.Handle(query, CancellationToken.None);
        Assert.NotNull(result);
        Assert.That(result?.VehicleId, Is.EqualTo(2));
        Assert.That(result?.ManufacturerName, Is.EqualTo("Honda"));
        Assert.That(result?.Model, Is.EqualTo("Civic"));
        Assert.That(result?.ModelYear, Is.EqualTo(2021));
        Assert.That(result?.ColourId, Is.EqualTo(ColourEnum.Blue));
    }

    [Test]
    public async Task Handle_ShouldReturnNull_WhenVehicleDoesNotExist()
    {
        var vehicles = new List<Vehicle>
        {
            new Vehicle { VehicleId = 1, ManufacturerName = "Toyota", Model = "Corolla", ModelYear = 2020 },
            new Vehicle { VehicleId = 2, ManufacturerName = "Honda", Model = "Civic", ModelYear = 2021 }
        }.AsQueryable();
        var mockVehicleDbSet = vehicles.BuildMockDbSet();
        _mockDbContext.Setup(db => db.Vehicles).Returns(mockVehicleDbSet.Object);
        var query = new GetVehicleByIdQuery { VehicleId = 999 };
        var result = await _handler.Handle(query, CancellationToken.None);
        Assert.IsNull(result); 
    }

    
}
