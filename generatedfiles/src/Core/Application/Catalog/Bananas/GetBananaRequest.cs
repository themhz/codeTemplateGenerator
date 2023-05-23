namespace FSH.Starter.Application.Catalog.Bananas;

public class GetBananaRequest : IRequest<BananaDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public GetBananaRequest(Guid id) => Id = id;
}

public class BananaByIdSpec : Specification<Banana, BananaDto>, ISingleResultSpecification
{
    public BananaByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetBananaRequestHandler : IRequestHandler<GetBananaRequest, BananaDto>
{
    private readonly IRepository<Banana> _repository;
    private readonly IStringLocalizer _t;

    public GetBananaRequestHandler(IRepository<Banana> repository, IStringLocalizer<GetBananaRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<BananaDto> Handle(GetBananaRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Banana, BananaDto>)new BananaByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Banana {0} Not Found.", request.Id]);
}
