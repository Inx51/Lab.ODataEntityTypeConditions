using Lab.ODataEntityTypeConditions.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab.ODataEntityTypeConditions.Data.EntityTypeConfigurations;

public class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(k => k.Id);

        builder.Property(p => p.Name)
            .IsRequired();
        
        builder.Property(p => p.Suffix)
            .IsRequired();

        builder.HasMany(p => p.Persons)
            .WithMany(p => p.Companies);
        
        builder.HasMany(p => p.Vehicles)
            .WithMany(p => p.Companies);
    }
}