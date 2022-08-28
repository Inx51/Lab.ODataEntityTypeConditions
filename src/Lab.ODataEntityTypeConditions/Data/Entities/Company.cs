using Lab.ODataEntityTypeConditions.Data.Entities.Bases;

namespace Lab.ODataEntityTypeConditions.Data.Entities;

public class Company : Entity
{
    public string? Name { get; set; }

    public string? Suffix { get; set; }
    
    
    public ICollection<Person>? Persons { get; set; }

    public ICollection<Vehicle>? Vehicles { get; set; }
}