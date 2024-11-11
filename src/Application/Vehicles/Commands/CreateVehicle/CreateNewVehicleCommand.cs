using MotusInterview.Application.Common.Dto;
using MotusInterview.Application.Common.Interfaces;
using MotusInterview.Application.Common.Models;
using MotusInterview.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MotusInterview.Application.Vehicles.Commands.CreateVehicle
{
    public class CreateNewVehicleCommand : IRequest<Result>
    {
        public required VehicleForm VehicleForm { get; init; }
    }


    public class CreateNewVehicleQueryHandler : IRequestHandler<CreateNewVehicleCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public CreateNewVehicleQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CreateNewVehicleCommand request, CancellationToken cancellationToken)
        {
            var newVehicle = new Vehicle
            {
                ManufacturerName = request.VehicleForm.ManufacturerName,
                Model = request.VehicleForm.Model,
                ModelYear = request.VehicleForm.ModelYear,
                ColourId = request.VehicleForm.ColourId ?? null
            };

            _context.Vehicles.Add(newVehicle);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

    }
}
