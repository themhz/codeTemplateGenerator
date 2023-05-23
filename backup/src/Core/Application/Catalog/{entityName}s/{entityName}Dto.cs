namespace FSH.Starter.Application.Catalog.{entityName}s;

public class {entityName}Dto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
