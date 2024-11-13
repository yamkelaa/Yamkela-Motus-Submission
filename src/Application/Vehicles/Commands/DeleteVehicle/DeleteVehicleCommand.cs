using MotusInterview.Application.Common.Interfaces;
using MotusInterview.Application.Common.Models;

namespace MotusInterview.Application.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommand : IRequest<Result>
    {
        public int VehicleId { get; set; }
    }

    public class DeleteVehicleByIdCommandHandler : IRequestHandler<DeleteVehicleCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteVehicleByIdCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _context.Vehicles
                .Where(v => v.VehicleId == request.VehicleId)
                .SingleOrDefaultAsync(cancellationToken);
            if (vehicle == null)
            {
                return Result.Failure(new[] { "Vehicle not found" });
            }
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
