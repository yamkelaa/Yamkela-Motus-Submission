using MotusInterview.Application.Common.Dto;
using MotusInterview.Application.Common.Interfaces;
using MotusInterview.Application.Common.Mappings;

namespace MotusInterview.Application.VehicleColours.Queries.GetAllColours
{
    public class GetAllColoursQuery : IRequest<List<ColoursDto>>
    {
    }

    public class GetAllColoursQueryHandler : IRequestHandler<GetAllColoursQuery, List<ColoursDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllColoursQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<ColoursDto>> Handle(GetAllColoursQuery request, CancellationToken cancellationToken)
        {
            return _context.Colours
                .OrderBy(c => c.ColourId)
                .ProjectToListAsync<ColoursDto>(_mapper.ConfigurationProvider);
        }
    }
}
