using System.Reflection;
using FSH.Starter.Application.Common.Interfaces;
using FSH.Starter.Domain.Catalog;
using FSH.Starter.Infrastructure.Persistence.Context;
using FSH.Starter.Infrastructure.Persistence.Initialization;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.Infrastructure.Catalog;

public class Bananaseeder : ICustomSeeder
{
    private readonly ISerializerService _serializerService;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<Bananaseeder> _logger;

    public Bananaseeder(ISerializerService serializerService, ILogger<Bananaseeder> logger, ApplicationDbContext db)
    {
        _serializerService = serializerService;
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (!_db.Bananas.Any())
        {
            _logger.LogInformation("Started to Seed Bananas table.");

            // Here you can use your own logic to populate the database.
            // As an example, I am using a JSON file to populate the database.
            string testData = await File.ReadAllTextAsync(path + "/Catalog/Bananadata.json", cancellationToken);
            var tests = _serializerService.Deserialize<List<Banana>>(testData);

            if (tests != null)
            {
                foreach (var test in tests)
                {
                    await _db.Bananas.AddAsync(test, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Bananas table.");
        }
    }
}
