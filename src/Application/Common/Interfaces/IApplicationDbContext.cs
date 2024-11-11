using MotusInterview.Domain.Entities;

namespace MotusInterview.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Vehicle> Vehicles { get; }
    DbSet<Colour> Colours { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
