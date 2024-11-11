using MotusInterview.Domain.Entities;

namespace MotusInterview.Application.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandValidator : AbstractValidator<UpdateVehicleCommand>
    {
        public UpdateVehicleCommandValidator()
        {
            RuleFor(x => x.VehicleForm.ManufacturerName)
                .NotEmpty().WithMessage("Manufacturer name is required. You cannot delete this value");

            RuleFor(x => x.VehicleForm.Model)
                .NotEmpty().WithMessage("Model is required. You cannot delete this value ");

            RuleFor(x => x.VehicleForm.ModelYear)
                .NotEmpty().WithMessage("Year is required. You cannot delete this value")
                .InclusiveBetween(2015, DateTime.Now.Year)
                .WithMessage("Model year must be between 2015 and the current year.");

            RuleFor(x => x.VehicleForm.ColourId)
                .Must(colour => !colour.HasValue || Enum.IsDefined(typeof(ColourEnum), colour.Value))
                .WithMessage("Colour must be a valid colour or null.");
        }
    }
}
