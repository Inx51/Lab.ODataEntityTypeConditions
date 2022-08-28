using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Lab.ODataEntityTypeConditions.Data.Entities.Bases;

namespace Lab.ODataEntityTypeConditions.Data.Entities;

public class Person : Entity
{
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }

    public int Age { get; set; }

    public string? Gender { get; set; }


    public ICollection<Company>? Companies { get; set; }

    public ICollection<Vehicle>? Vehicles { get; set; }
}