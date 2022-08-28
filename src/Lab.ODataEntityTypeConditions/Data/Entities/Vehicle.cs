using Lab.ODataEntityTypeConditions.Data.Entities.Bases;

namespace Lab.ODataEntityTypeConditions.Data.Entities;

public class Vehicle : Entity
{
    public string? Manufacturer { get; set; }

    public string? Model { get; set; }
    
    public string? Type { get; set; }
    
    
    public ICollection<Person>? Persons { get; set; }

    public ICollection<Company>? Companies { get; set; }
}