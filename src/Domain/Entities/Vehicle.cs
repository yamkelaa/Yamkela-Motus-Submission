using System.Drawing;

namespace MotusInterview.Domain.Entities;

public class Vehicle
{
    public int VehicleId { get; set; }
    public required string ManufacturerName { get; set; }
    public required string Model { get; set; }
    public required int ModelYear { get; set; }
    public ColourEnum? ColourId { get; set; }
    public virtual Colour? Colour { get; set; }
}
