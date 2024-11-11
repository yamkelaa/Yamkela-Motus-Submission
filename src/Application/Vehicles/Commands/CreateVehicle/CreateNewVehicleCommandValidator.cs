using FluentValidation;
using MotusInterview.Domain.Entities;

namespace MotusInterview.Application.Vehicles.Commands.CreateVehicle
{
    public class CreateNewVheicleCommandValidator : AbstractValidator<CreateNewVehicleCommand>
    {
        public CreateNewVheicleCommandValidator()
        {
            RuleFor(x => x.VehicleForm.ManufacturerName)
                .NotEmpty().WithMessage("Manufacturer name is required.");

            RuleFor(x => x.VehicleForm.Model)
                .NotEmpty().WithMessage("Model is required.");

            RuleFor(x => x.VehicleForm.ModelYear)
                .NotEmpty().WithMessage("Year is required.")
                .InclusiveBetween(2015, DateTime.Now.Year)
                .WithMessage("Model year must be between 2015 and the current year.");

            RuleFor(x => x.VehicleForm.ColourId)
                  .Must(colour => !colour.HasValue || Enum.IsDefined(typeof(ColourEnum), colour.Value))
                  .WithMessage("Colour must be a valid colour or null.");
        }
    }
}
