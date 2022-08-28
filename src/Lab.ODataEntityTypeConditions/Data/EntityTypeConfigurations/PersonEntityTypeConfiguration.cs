using Lab.ODataEntityTypeConditions.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab.ODataEntityTypeConditions.Data.EntityTypeConfigurations;

public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(k => k.Id);

        builder.Property(p => p.Age)
            .IsRequired();
        
        builder.Property(p => p.Gender)
            .IsRequired();

        builder.Property(p => p.FirstName)
            .IsRequired();

        builder.Property(p => p.LastName)
            .IsRequired();

        builder.HasMany(p => p.Companies)
            .WithMany(p => p.Persons);
        
        builder.HasMany(p => p.Vehicles)
            .WithMany(p => p.Persons);
    }
}