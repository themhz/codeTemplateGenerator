namespace FSH.Starter.Application.Catalog.Bananas;

public class BananaByNameSpec : Specification<Banana>, ISingleResultSpecification
{
    public BananaByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}
