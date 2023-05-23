using System.ComponentModel;

namespace FSH.Starter.Application.Catalog.Bananas;

public interface IBananaGeneratorJob : IScopedService
{
    [DisplayName("Generate Random Banana example job on Queue notDefault")]
    Task GenerateAsync(int nSeed, CancellationToken cancellationToken);

    [DisplayName("removes all random Bananas created example job on Queue notDefault")]
    Task CleanAsync(CancellationToken cancellationToken);
}
