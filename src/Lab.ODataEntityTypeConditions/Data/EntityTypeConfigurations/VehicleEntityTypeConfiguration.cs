using Lab.ODataEntityTypeConditions.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab.ODataEntityTypeConditions.Data.EntityTypeConfigurations;

public class VehicleEntityTypeConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(k => k.Id);

        builder.Property(p => p.Model)
            .IsRequired();
        
        builder.Property(p => p.Type)
            .IsRequired();
        
        builder.Property(p => p.Manufacturer)
            .IsRequired();

        builder.HasMany(p => p.Persons)
            .WithMany(p => p.Vehicles);
        
        builder.HasMany(p => p.Companies)
            .WithMany(p => p.Vehicles);
    }
}