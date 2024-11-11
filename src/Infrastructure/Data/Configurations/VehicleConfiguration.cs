using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotusInterview.Domain.Entities;

namespace MotusInterview.Infrastructure.Data.Configurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("Vehicles", "dbo");

            builder.HasKey(vehicle => vehicle.VehicleId);

            builder.Property(vehicle => vehicle.VehicleId)
            .HasColumnType("INT")
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

            builder.Property(vehicle => vehicle.ManufacturerName)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder.Property(vehicle => vehicle.Model)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();

            builder.Property(vehicle => vehicle.ModelYear)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(vehicle => vehicle.ColourId)
                .HasColumnType("INT")
                .IsRequired(false);

            builder.HasOne(vehicle => vehicle.Colour)
                .WithMany()
                .HasForeignKey(vehicle => vehicle.ColourId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
