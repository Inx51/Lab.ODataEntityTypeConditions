using Lab.ODataEntityTypeConditions.Data;
using Lab.ODataEntityTypeConditions.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Lab.ODataEntityTypeConditions.Controllers;

public class CompaniesController : ControllerBase
{
    private readonly DatabaseContext _databaseContext;

    public CompaniesController(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    [HttpGet]
    [EnableQuery(PageSize = 50)]
    public IQueryable<Company> Get(ODataQueryOptions<Company> queryOptions)
    {
        return _databaseContext.Companies!;
    }
}