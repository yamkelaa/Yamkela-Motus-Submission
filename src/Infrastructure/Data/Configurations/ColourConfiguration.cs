using System.Drawing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotusInterview.Domain.Entities;

namespace MotusInterview.Infrastructure.Data.Configurations
{
    public class ColourConfiguration : IEntityTypeConfiguration<Colour>
    {
        public void Configure(EntityTypeBuilder<Colour> builder)
        {
            builder.ToTable("Colours", "dbo");
            builder.HasKey(colour => colour.ColourId);
            builder.Property(colour => colour.ColourId)
                .HasColumnType("INT")
                .ValueGeneratedOnAdd();
            builder.Property(colour => colour.ColourName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();
            builder.Property(colour => colour.ColourHex)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsRequired();
        }
    }
}

