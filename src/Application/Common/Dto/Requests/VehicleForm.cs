using MotusInterview.Domain.Entities;

namespace MotusInterview.Application.Common.Models;

public class VehicleForm
{
    public required string ManufacturerName { get; init; }
    public required string Model { get; init; }
    public required int ModelYear { get; init; }
    public ColourEnum? ColourId { get; init; }
}
