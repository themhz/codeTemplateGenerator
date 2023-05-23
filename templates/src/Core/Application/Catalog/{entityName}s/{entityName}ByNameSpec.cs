namespace FSH.Starter.Application.Catalog.{entityName}s;

public class {entityName}ByNameSpec : Specification<{entityName}>, ISingleResultSpecification
{
    public {entityName}ByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}
