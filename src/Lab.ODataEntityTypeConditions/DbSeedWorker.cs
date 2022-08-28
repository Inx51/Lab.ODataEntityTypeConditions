using Bogus;
using Lab.ODataEntityTypeConditions.Data;
using Lab.ODataEntityTypeConditions.Data.Entities;
using Lab.ODataEntityTypeConditions.Extensions;

namespace Lab.ODataEntityTypeConditions;

public class DbSeedWorker : BackgroundService
{
    private readonly ILogger<DbSeedWorker> _logger;
    private readonly DatabaseContext _databaseContext;

    private const int Seed = 100;
    private const bool FixedData = true;
    
    private const float OddsOfInCompany = 0.8f;
    private const float OddsOfInSecondCompany = 0.05f;
    private const float OddsOfVehicle = 0.6f;
    private const float OddsOfSecondVehicle = 0.4f;
    private const float OddsOfThirdVehicle = 0.1f;
    private const float OddsOfCompanyVehicle = 0.3f;
    
    public DbSeedWorker
    (
        ILogger<DbSeedWorker> logger,
        DatabaseContext databaseContext
    )
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run
        (
            async () =>
            {   
                await RunWorkAsync(stoppingToken);
            },
            stoppingToken
        );
    }

    private async Task RunWorkAsync(CancellationToken stoppingToken)
    {
        var seed = Seed;
#pragma warning disable CS0162
        // ReSharper disable once HeuristicUnreachableCode
        var randomizer = FixedData ? new Randomizer(seed) : new Randomizer();
#pragma warning restore CS0162
        var faker = new Faker();
        
        _logger.LogInformation<DbSeedWorker>("Starting to seed database..");
        
        var vehicles = GetVehicles(randomizer, seed);
        var companies = GetCompanies(randomizer, seed);
        var persons = GetPersons(randomizer, seed);
        
        foreach (var person in persons)
        {
            SetCompany(person, companies, randomizer, faker);
            SetVehicle(person, vehicles, randomizer, faker);
        }
        
        await _databaseContext.Persons!.AddRangeAsync(persons, stoppingToken);
        await _databaseContext.SaveChangesAsync(stoppingToken);
        
        _logger.LogInformation<DbSeedWorker>("Seeding database completed..");
    }

    private void SetVehicle(Data.Entities.Person person, List<Vehicle> vehicles, Randomizer randomizer, Faker faker)
    {
        if (randomizer.Bool(OddsOfVehicle))
        {
            person.Vehicles = new List<Vehicle>();
            AddVehicle(person, vehicles, randomizer, faker);
        }
        
        if (randomizer.Bool(OddsOfSecondVehicle) && person.Vehicles is not null && person.Vehicles!.Count == 1)
            AddVehicle(person, vehicles, randomizer, faker);
        
        if (randomizer.Bool(OddsOfThirdVehicle) && person.Vehicles is not null &&  person.Vehicles!.Count == 2)
            AddVehicle(person, vehicles, randomizer, faker);
    }

    private void AddVehicle(Data.Entities.Person person, List<Vehicle> vehicles, Randomizer randomizer, Faker faker)
    {
        if (vehicles.Count > 0)
        {
            var vehicle = faker.PickRandom(vehicles);
            person.Vehicles!.Add(vehicle);
            if (randomizer.Bool(OddsOfCompanyVehicle) && person.Companies is not null && person.Companies!.Count > 0)
                AddVehicleToRandomCompany(faker, person.Companies!, vehicle);

            vehicles.Remove(vehicle);
        }
    }

    private void AddVehicleToRandomCompany(Faker faker, ICollection<Company> personCompanies, Vehicle vehicle)
    {
        var company = faker.PickRandom(personCompanies);

        if (company.Vehicles == null)
            company.Vehicles = new List<Vehicle>();
        
        company.Vehicles!.Add(vehicle);
    }

    private void SetCompany(Data.Entities.Person person, List<Company> companies, Randomizer randomizer, Faker faker)
    {
        if (randomizer.Bool(OddsOfInCompany))
        {
            person.Companies = new List<Company>();
            
            var company = faker.PickRandom(companies);
            person.Companies.Add(company);
        }
        
        if (randomizer.Bool(OddsOfInSecondCompany) && person.Companies is not null && person.Companies.Count == 1)
            person.Companies.Add(faker.PickRandom(companies.Except(person.Companies)));
    }

    private List<Vehicle> GetVehicles(Randomizer randomizer, int seed)
    {
        var vehicleFaker = GetVehicleFaker(seed);
        return vehicleFaker.Generate(randomizer.Number(122, 500));
    }
    
    private List<Company> GetCompanies(Randomizer randomizer, int seed)
    {
        var companyFaker = GetCompanyFaker(seed);
        return companyFaker.Generate(randomizer.Number(11, 22));
    }
    
    private List<Data.Entities.Person> GetPersons(Randomizer randomizer, int seed)
    {
        var personFaker = GetPersonFaker(seed);
        return personFaker.Generate(randomizer.Number(223, 2302));
    }
    
    private Faker<Vehicle> GetVehicleFaker(int seed)
    {
        return new Faker<Vehicle>()
            .UseSeed(seed)
            .RuleFor(p => p.Id, f => f.IndexFaker + 1000)
            .RuleFor(p => p.Manufacturer, f => f.Vehicle.Manufacturer())
            .RuleFor(p => p.Model, f => f.Vehicle.Model())
            .RuleFor(p => p.Type, f => f.Vehicle.Type());
    }
    
    private Faker<Company> GetCompanyFaker(int seed)
    {
        return new Faker<Company>()
            .UseSeed(seed)
            .RuleFor(p => p.Id, f => f.IndexFaker + 2000)
            .RuleFor(p => p.Name, f => f.Company.CompanyName())
            .RuleFor(p => p.Suffix, f => f.Company.CompanySuffix());
    }
    
    private Faker<Data.Entities.Person> GetPersonFaker(int seed)
    {
        return new Faker<Data.Entities.Person>()
            .UseSeed(seed)
            .RuleFor(p => p.Id, f => f.IndexFaker + 3000)
            .RuleFor(p => p.FirstName, f => f.Person.FirstName)
            .RuleFor(p => p.LastName, f => f.Person.LastName)
            .RuleFor(p => p.Gender, f => f.Person.Gender.ToString())
            .RuleFor(p => p.Age, f => f.Random.Number(15, 75));
    }
}