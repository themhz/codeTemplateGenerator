namespace FSH.Starter.Application.Catalog.{entityName}s;

public class Get{entityName}Request : IRequest<{entityName}Dto>
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public Get{entityName}Request(Guid id) => Id = id;
}

public class {entityName}ByIdSpec : Specification<{entityName}, {entityName}Dto>, ISingleResultSpecification
{
    public {entityName}ByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class Get{entityName}RequestHandler : IRequestHandler<Get{entityName}Request, {entityName}Dto>
{
    private readonly IRepository<{entityName}> _repository;
    private readonly IStringLocalizer _t;

    public Get{entityName}RequestHandler(IRepository<{entityName}> repository, IStringLocalizer<Get{entityName}RequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<{entityName}Dto> Handle(Get{entityName}Request request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<{entityName}, {entityName}Dto>)new {entityName}ByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["{entityName} {0} Not Found.", request.Id]);
}
