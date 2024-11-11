using MotusInterview.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddHttpContextAccessor();
        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();
        services.AddExceptionHandler<CustomExceptionHandler>();
        
        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddRazorPages();
        services.AddEndpointsApiExplorer();
        services.AddOpenApiDocument((configure, sp) =>
        {
            configure.Title = "MotusInterview API";

        });

        return services;
    }
}
