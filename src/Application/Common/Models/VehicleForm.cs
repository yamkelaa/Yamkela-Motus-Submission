using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotusInterview.Domain.Entities;

namespace MotusInterview.Application.Common.Models;

/// <summary>
///This class was designed to prvide strcuture for the values a user must provide when creating or updating a vehicle
///It also avoids repition as the create an update form accept similar input values, (with updating including the vehicleId in the route)
/// </summary>
public class VehicleForm
{
    public required string ManufacturerName { get; init; }
    public required string Model { get; init; }
    public required int ModelYear { get; init; }
    public ColourEnum? ColourId { get; init; }
}
