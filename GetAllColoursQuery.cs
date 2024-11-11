using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotusInterview.Application.Common.Dto;
using MotusInterview.Application.Common.Interfaces;
using MotusInterview.Application.Common.Mappings;
using MotusInterview.Application.Common.Models;
using MotusInterview.Application.Vehicles.Queries.GetVehiclesWithPagination;
using MediatR;
using AutoMapper;
using System.Threading;

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
