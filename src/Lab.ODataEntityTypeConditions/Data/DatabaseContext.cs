using Lab.ODataEntityTypeConditions.Data.Entities;
using Lab.ODataEntityTypeConditions.Data.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Lab.ODataEntityTypeConditions.Data;

public class DatabaseContext : DbContext
{
    
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    public DbSet<Person>? Persons { get; set; }

    public DbSet<Company>? Companies { get; set; }
    
    public DbSet<Vehicle>? Vehicles { get; set; }
    // ReSharper restore UnusedAutoPropertyAccessor.Global

    public DatabaseContext(DbContextOptions options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PersonEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleEntityTypeConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}