using MotusInterview.Application.Common.Interfaces;
using MotusInterview.Application.Common.Dto;


namespace MotusInterview.Application.Vehicles.Queries.GetVehicleById
{
    public class GetVehicleByIdQuery : IRequest<VehiclesDto?>
    {
        public int VehicleId { get; set; }
    }

    public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, VehiclesDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetVehicleByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<VehiclesDto?> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            return _context.Vehicles
            .Where(v => v.VehicleId == request.VehicleId)
            .ProjectTo<VehiclesDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        }

    }
}
