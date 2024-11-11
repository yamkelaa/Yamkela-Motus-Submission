namespace MotusInterview.Application.Vehicles.Queries.GetVehiclesWithPagination;

public class GetVehiclesWithPaginationQueryValidator : AbstractValidator<GetVehiclesWithPaginationQuery>
{
    public GetVehiclesWithPaginationQueryValidator()
    {
        RuleFor(m => m.PageNumber)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0.");
        
        RuleFor(m => m.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than 0.");
    }
}
