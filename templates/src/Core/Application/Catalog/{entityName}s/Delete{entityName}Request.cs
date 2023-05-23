namespace FSH.Starter.Application.Catalog.{entityName}s;

public class Delete{entityName}Request : IRequest<Guid>
{
    public Guid Id { get; set; }

    public Delete{entityName}Request(Guid id) => Id = id;
}

public class Delete{entityName}RequestHandler : IRequestHandler<Delete{entityName}Request, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<{entityName}> _{entityName}Repo;
    private readonly IReadRepository<Product> _productRepo;
    private readonly IStringLocalizer _t;

    public Delete{entityName}RequestHandler(IRepositoryWithEvents<{entityName}> {entityName}Repo, IReadRepository<Product> productRepo, IStringLocalizer<Delete{entityName}RequestHandler> localizer) =>
        (_{entityName}Repo, _productRepo, _t) = ({entityName}Repo, productRepo, localizer);

    public async Task<Guid> Handle(Delete{entityName}Request request, CancellationToken cancellationToken)
    {    

        var {entityName} = await _{entityName}Repo.GetByIdAsync(request.Id, cancellationToken);

        _ = {entityName} ?? throw new NotFoundException(_t["{entityName} {0} Not Found."]);

        await _{entityName}Repo.DeleteAsync({entityName}, cancellationToken);

        return request.Id;
    }
}
