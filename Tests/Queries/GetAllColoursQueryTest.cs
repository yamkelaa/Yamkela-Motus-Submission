using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MotusInterview.Application.Common.Interfaces;
using MotusInterview.Application.VehicleColours.Queries.GetAllColours;
using MotusInterview.Application.Common.Dto;
using MockQueryable.Moq;
using MotusInterview.Domain.Entities;

namespace MotusInterview.Tests.VehicleColours.Queries
{
    [TestFixture]
    public class GetAllColoursQueryHandlerTests
    {
        private Mock<IApplicationDbContext> _mockDbContext;
        private IMapper _mapper;
        private GetAllColoursQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockDbContext = new Mock<IApplicationDbContext>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Colour, ColoursDto>(); 
            });

            _mapper = mapperConfig.CreateMapper(); 
            _handler = new GetAllColoursQueryHandler(_mockDbContext.Object, _mapper);
        }

        [Test]
        public async Task Handle_ShouldReturnColoursList_WhenColoursExist()
        {
            
            var colours = new List<Colour>
            {
                new Colour { ColourId = ColourEnum.Red, ColourName = "Red", ColourHex = "#FF0000" },
                new Colour { ColourId = ColourEnum.Blue, ColourName = "Blue", ColourHex = "#0000FF" },
                new Colour { ColourId = ColourEnum.Green, ColourName = "Green", ColourHex = "#00FF00" }
            }.AsQueryable();
            var mockColourDbSet = colours.BuildMockDbSet();
            _mockDbContext.Setup(db => db.Colours).Returns(mockColourDbSet.Object);

            var query = new GetAllColoursQuery();
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0].ColourId, Is.EqualTo(ColourEnum.Red));
            Assert.That(result[0].ColourName, Is.EqualTo("Red"));
            Assert.That(result[0].ColourHex, Is.EqualTo("#FF0000"));
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyList_WhenNoColoursExist()
        {
            var colours = new List<Colour>().AsQueryable();

            var mockColourDbSet = colours.BuildMockDbSet();

            _mockDbContext.Setup(db => db.Colours).Returns(mockColourDbSet.Object);

            var query = new GetAllColoursQuery();
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.That(result.Count, Is.EqualTo(0)); 
        }
    }
}
