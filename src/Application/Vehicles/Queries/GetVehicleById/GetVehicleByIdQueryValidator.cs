
namespace MotusInterview.Application.Vehicles.Queries.GetVehicleById
{
    public class GetVehicleByIdQueryValidator : AbstractValidator<GetVehicleByIdQuery>
    {
        public GetVehicleByIdQueryValidator()
        {
            RuleFor(v => v.VehicleId)
                .GreaterThan(0)
                .WithMessage("Vehicle ID must be greater than 0.");
        }
    }
}
