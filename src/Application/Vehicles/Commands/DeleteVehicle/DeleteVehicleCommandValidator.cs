namespace MotusInterview.Application.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommandValidator : AbstractValidator<DeleteVehicleCommand>
    {
        public DeleteVehicleCommandValidator()
        {
            RuleFor(v => v.VehicleId)
                .GreaterThan(0)
                .WithMessage("Vehicle ID must be greater than 0.");
        }
    }
}
