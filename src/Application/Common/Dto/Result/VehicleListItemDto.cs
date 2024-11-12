using MotusInterview.Domain.Entities;

namespace MotusInterview.Application.Common.Dto;

public class VehicleListItemDto
{
    public int VehicleId { get; init; }
    public required string ManufacturerName { get; init; }
    public required string Model { get; init; }
    public required int ModelYear { get; init; }
    public ColoursDto? Colour { get; init; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Vehicle, VehicleListItemDto>()
            .ForMember(dest => dest.Colour, opt => opt.MapFrom(src =>
                src.Colour != null ? new ColoursDto
                {
                    ColourId = src.Colour.ColourId,
                    ColourName = src.Colour.ColourName,
                    ColourHex = src.Colour.ColourHex
                }
                : null));
        }
    }
}
