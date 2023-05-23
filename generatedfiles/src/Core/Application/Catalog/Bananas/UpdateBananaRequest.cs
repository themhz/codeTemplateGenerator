namespace FSH.Starter.Application.Catalog.Bananas;

public class UpdateBananaRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

public class UpdateBananaRequestValidator : CustomValidator<UpdateBananaRequest>
{
    public UpdateBananaRequestValidator(IRepository<Banana> repository, IStringLocalizer<UpdateBananaRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (Banana, name, ct) =>
                    await repository.FirstOrDefaultAsync(new BananaByNameSpec(name), ct)
                        is not Banana existingBanana || existingBanana.Id == Banana.Id)
                .WithMessage((_, name) => T["Banana {0} already Exists.", name]);
}

public class UpdateBananaRequestHandler : IRequestHandler<UpdateBananaRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Banana> _repository;
    private readonly IStringLocalizer _t;

    public UpdateBananaRequestHandler(IRepositoryWithEvents<Banana> repository, IStringLocalizer<UpdateBananaRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateBananaRequest request, CancellationToken cancellationToken)
    {
        var Banana = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = Banana
        ?? throw new NotFoundException(_t["Banana {0} Not Found.", request.Id]);

        Banana.Update(request.Name, request.Description);

        await _repository.UpdateAsync(Banana, cancellationToken);

        return request.Id;
    }
}
