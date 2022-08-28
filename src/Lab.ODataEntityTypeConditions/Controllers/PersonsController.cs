using Lab.ODataEntityTypeConditions.Data;
using Lab.ODataEntityTypeConditions.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Lab.ODataEntityTypeConditions.Controllers;

public class PersonsController : ControllerBase
{
    private readonly DatabaseContext _databaseContext;
    
    public PersonsController(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    [HttpGet]
    [EnableQuery(PageSize = 50)]
    public IQueryable<Person> Get(ODataQueryOptions<Person> queryOptions)
    {
        return _databaseContext.Persons!;
    }
}