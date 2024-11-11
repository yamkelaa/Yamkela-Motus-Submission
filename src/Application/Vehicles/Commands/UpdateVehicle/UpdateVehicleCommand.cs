using System;
using System.Threading;
using System.Threading.Tasks;
using MotusInterview.Application.Common.Interfaces;
using MotusInterview.Application.Common.Models;
using MotusInterview.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MotusInterview.Application.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommand : IRequest<Result>
    {
        public int VehicleId { get; init; }
        public required VehicleForm VehicleForm { get; init; }
    }

    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public UpdateVehicleCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(v => v.VehicleId == request.VehicleId, cancellationToken);

            if (vehicle == null)
            {
                return Result.Failure(new[] { "The vehicle you are trying to update does not exist." });
            }
            vehicle.ManufacturerName = request.VehicleForm.ManufacturerName;
            vehicle.Model = request.VehicleForm.Model;
            vehicle.ModelYear = request.VehicleForm.ModelYear;
            vehicle.ColourId = request.VehicleForm.ColourId;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
