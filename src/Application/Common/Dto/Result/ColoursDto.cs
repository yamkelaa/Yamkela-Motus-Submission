using MotusInterview.Domain.Entities;

namespace MotusInterview.Application.Common.Dto.RequestDto;
public class ColoursDto
{
    public ColourEnum ColourId { get; init; }
    public required string ColourName { get; init; }
    public required string? ColourHex { get; init; }
}

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Colour, ColoursDto>();
    }
}
