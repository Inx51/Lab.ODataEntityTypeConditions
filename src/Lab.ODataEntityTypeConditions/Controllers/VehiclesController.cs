using Lab.ODataEntityTypeConditions.Data;
using Lab.ODataEntityTypeConditions.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Lab.ODataEntityTypeConditions.Controllers;

public class VehiclesController : ControllerBase
{
    private readonly DatabaseContext _databaseContext;
    
    public VehiclesController(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    [HttpGet]
    [EnableQuery(PageSize = 50)]
    public IQueryable<Vehicle> Get()
    {
        return _databaseContext.Vehicles!;
    }
}