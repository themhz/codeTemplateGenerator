using System.ComponentModel;

namespace FSH.Starter.Application.Catalog.{entityName}s;

public interface I{entityName}GeneratorJob : IScopedService
{
    [DisplayName("Generate Random {entityName} example job on Queue notDefault")]
    Task GenerateAsync(int nSeed, CancellationToken cancellationToken);

    [DisplayName("removes all random {entityName}s created example job on Queue notDefault")]
    Task CleanAsync(CancellationToken cancellationToken);
}
