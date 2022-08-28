using Lab.ODataEntityTypeConditions;
using Lab.ODataEntityTypeConditions.Data;
using Lab.ODataEntityTypeConditions.Data.Entities;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase("Database"));
builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("odata", GetEdmModel()).EnableQueryFeatures(50));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddHostedService<DbSeedWorker>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();

    builder.EntitySet<Person>("Persons");
    builder.EntitySet<Company>("Companies");
    builder.EntitySet<Vehicle>("Vehicles");

    return builder.GetEdmModel();
}