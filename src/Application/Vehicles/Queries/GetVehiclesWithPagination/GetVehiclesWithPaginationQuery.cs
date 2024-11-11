using MotusInterview.Application.Common.Dto;
using MotusInterview.Application.Common.Interfaces;
using MotusInterview.Application.Common.Mappings;
using MotusInterview.Application.Common.Models;

namespace MotusInterview.Application.Vehicles.Queries.GetVehiclesWithPagination;

public class GetVehiclesWithPaginationQuery : IRequest<PaginatedList<VehicleListItemDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetVehiclesWithPaginationQueryHandler : IRequestHandler<GetVehiclesWithPaginationQuery, PaginatedList<VehicleListItemDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetVehiclesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<PaginatedList<VehicleListItemDto>> Handle(GetVehiclesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return _context.Vehicles
            .OrderBy(v => v.VehicleId)
            .ProjectTo<VehicleListItemDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
