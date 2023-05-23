namespace FSH.Starter.Application.Catalog.Bananas;

public class CreateBananaRequest : IRequest<Guid>
{
    public string? Name { get; set; } = default!;
    public string? Description { get; set; }
}

public class CreateBananaRequestValidator : CustomValidator<CreateBananaRequest>
{
    public CreateBananaRequestValidator(IReadRepository<Banana> repository, IStringLocalizer<CreateBananaRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.FirstOrDefaultAsync(new BananaByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Banana {0} already Exists.", name]);
}

public class CreateBananaRequestHandler : IRequestHandler<CreateBananaRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Banana> _repository;

    public CreateBananaRequestHandler(IRepositoryWithEvents<Banana> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateBananaRequest request, CancellationToken cancellationToken)
    {
        var Banana = new Banana(request.Name, request.Description);

        await _repository.AddAsync(Banana, cancellationToken);

        return Banana.Id;
    }
}
