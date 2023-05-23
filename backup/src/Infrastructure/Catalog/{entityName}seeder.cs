using System.Reflection;
using FSH.Starter.Application.Common.Interfaces;
using FSH.Starter.Domain.Catalog;
using FSH.Starter.Infrastructure.Persistence.Context;
using FSH.Starter.Infrastructure.Persistence.Initialization;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.Infrastructure.Catalog;

public class {entityName}seeder : ICustomSeeder
{
    private readonly ISerializerService _serializerService;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<{entityName}seeder> _logger;

    public {entityName}seeder(ISerializerService serializerService, ILogger<{entityName}seeder> logger, ApplicationDbContext db)
    {
        _serializerService = serializerService;
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (!_db.{entityName}s.Any())
        {
            _logger.LogInformation("Started to Seed {entityName}s table.");

            // Here you can use your own logic to populate the database.
            // As an example, I am using a JSON file to populate the database.
            string testData = await File.ReadAllTextAsync(path + "/Catalog/{entityName}data.json", cancellationToken);
            var tests = _serializerService.Deserialize<List<{entityName}>>(testData);

            if (tests != null)
            {
                foreach (var test in tests)
                {
                    await _db.{entityName}s.AddAsync(test, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded {entityName}s table.");
        }
    }
}
