namespace FSH.Starter.Application.Catalog.Bananas;

public class BananaDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
