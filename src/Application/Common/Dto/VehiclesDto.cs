using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotusInterview.Domain.Entities;

namespace MotusInterview.Application.Common.Dto;
public class VehiclesDto
{
    public int VehicleId { get; init; }
    public required string ManufacturerName { get; init; }
    public required string Model { get; init; }
    public int ModelYear { get; init; }
    public ColourEnum? ColourId { get; init; } = null;

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Vehicle, VehiclesDto>()
                .ForMember(dest => dest.ColourId, opt => opt.MapFrom(src => src.ColourId.HasValue ? (ColourEnum?)src.ColourId : null));
        }
    }
}



