using Microsoft.AspNetCore.Mvc;
using MotusInterview.Application.Common.Dto;
using MotusInterview.Application.Common.Models;
using MotusInterview.Application.Vehicles.Commands.CreateVehicle;
using MotusInterview.Application.Vehicles.Commands.DeleteVehicle;
using MotusInterview.Application.Vehicles.Commands.UpdateVehicle;
using MotusInterview.Application.Vehicles.Queries.GetVehicleById;
using MotusInterview.Application.Vehicles.Queries.GetVehiclesWithPagination;


public class Vehicles : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetVehiclesWithPagination);

        app.MapGroup(this)
            .MapGet("{vehicleId}", GetVehicleById);

        app.MapGroup(this)
            .MapPost(CreateNewVehicle);

        app.MapGroup(this)
           .MapPut("{vehicleId}", UpdateVehicle);

        app.MapGroup(this)
            .MapDelete("{vehicleId}", DeleteVehicle);
    }

    public Task<PaginatedList<VehicleListItemDto>> GetVehiclesWithPagination(ISender sender, [AsParameters] GetVehiclesWithPaginationQuery query)
    {
        return sender.Send(query);
    }

    public Task<VehiclesDto?> GetVehicleById(ISender sender, int vehicleId)
    {
        var query = new GetVehicleByIdQuery { VehicleId = vehicleId };
        return sender.Send(query);
    }

    public Task<Result> CreateNewVehicle(ISender sender, VehicleForm query)
    {
        var createRequest = new CreateNewVehicleCommand { VehicleForm = query };
        return sender.Send(createRequest);
    }

    public Task<Result> UpdateVehicle(ISender sender, [FromRoute] int vehicleId, VehicleForm query)
    {
        var updateRequest = new UpdateVehicleCommand
        {
            VehicleId = vehicleId,
            VehicleForm = query
        };

        return sender.Send(updateRequest);
    }

    public Task<Result> DeleteVehicle(ISender sender, int vehicleId)
    {
        var query = new DeleteVehicleCommand { VehicleId = vehicleId };
        return sender.Send(query);
    }
}
