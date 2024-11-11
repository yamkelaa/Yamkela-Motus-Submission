using System.Reflection;
using MotusInterview.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using MotusInterview.Domain.Entities;

namespace MotusInterview.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Colour> Colours => Set<Colour>();


    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
