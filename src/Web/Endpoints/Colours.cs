using MotusInterview.Application.Common.Dto;
using MotusInterview.Application.VehicleColours.Queries.GetAllColours;

namespace MotusInterview.Web.Endpoints;

public class Colours : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetAllColoursQuery);
    }
    public Task<List<ColoursDto>> GetAllColoursQuery(ISender sender, [AsParameters] GetAllColoursQuery query)
    {
        return sender.Send(query);
    }
}
