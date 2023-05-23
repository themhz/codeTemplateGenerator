namespace FSH.Starter.Application.Catalog.Bananas;

public class SearchBananasRequest : PaginationFilter, IRequest<PaginationResponse<BananaDto>>
{
}

public class BananasBySearchRequestSpec : EntitiesByPaginationFilterSpec<Banana, BananaDto>
{
    public BananasBySearchRequestSpec(SearchBananasRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchBananasRequestHandler : IRequestHandler<SearchBananasRequest, PaginationResponse<BananaDto>>
{
    private readonly IReadRepository<Banana> _repository;

    public SearchBananasRequestHandler(IReadRepository<Banana> repository) => _repository = repository;

    public async Task<PaginationResponse<BananaDto>> Handle(SearchBananasRequest request, CancellationToken cancellationToken)
    {
        var spec = new BananasBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}
