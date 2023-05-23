namespace FSH.Starter.Application.Catalog.Bananas;

public class DeleteBananaRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteBananaRequest(Guid id) => Id = id;
}

public class DeleteBananaRequestHandler : IRequestHandler<DeleteBananaRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Banana> _BananaRepo;
    private readonly IReadRepository<Product> _productRepo;
    private readonly IStringLocalizer _t;

    public DeleteBananaRequestHandler(IRepositoryWithEvents<Banana> BananaRepo, IReadRepository<Product> productRepo, IStringLocalizer<DeleteBananaRequestHandler> localizer) =>
        (_BananaRepo, _productRepo, _t) = (BananaRepo, productRepo, localizer);

    public async Task<Guid> Handle(DeleteBananaRequest request, CancellationToken cancellationToken)
    {    

        var Banana = await _BananaRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = Banana ?? throw new NotFoundException(_t["Banana {0} Not Found."]);

        await _BananaRepo.DeleteAsync(Banana, cancellationToken);

        return request.Id;
    }
}
