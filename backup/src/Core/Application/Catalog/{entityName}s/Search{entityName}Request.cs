namespace FSH.Starter.Application.Catalog.{entityName}s;

public class Search{entityName}sRequest : PaginationFilter, IRequest<PaginationResponse<{entityName}Dto>>
{
}

public class {entityName}sBySearchRequestSpec : EntitiesByPaginationFilterSpec<{entityName}, {entityName}Dto>
{
    public {entityName}sBySearchRequestSpec(Search{entityName}sRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class Search{entityName}sRequestHandler : IRequestHandler<Search{entityName}sRequest, PaginationResponse<{entityName}Dto>>
{
    private readonly IReadRepository<{entityName}> _repository;

    public Search{entityName}sRequestHandler(IReadRepository<{entityName}> repository) => _repository = repository;

    public async Task<PaginationResponse<{entityName}Dto>> Handle(Search{entityName}sRequest request, CancellationToken cancellationToken)
    {
        var spec = new {entityName}sBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}
