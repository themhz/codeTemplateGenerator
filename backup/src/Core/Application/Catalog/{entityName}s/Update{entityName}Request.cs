namespace FSH.Starter.Application.Catalog.{entityName}s;

public class Update{entityName}Request : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

public class Update{entityName}RequestValidator : CustomValidator<Update{entityName}Request>
{
    public Update{entityName}RequestValidator(IRepository<{entityName}> repository, IStringLocalizer<Update{entityName}RequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async ({entityName}, name, ct) =>
                    await repository.FirstOrDefaultAsync(new {entityName}ByNameSpec(name), ct)
                        is not {entityName} existing{entityName} || existing{entityName}.Id == {entityName}.Id)
                .WithMessage((_, name) => T["{entityName} {0} already Exists.", name]);
}

public class Update{entityName}RequestHandler : IRequestHandler<Update{entityName}Request, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<{entityName}> _repository;
    private readonly IStringLocalizer _t;

    public Update{entityName}RequestHandler(IRepositoryWithEvents<{entityName}> repository, IStringLocalizer<Update{entityName}RequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(Update{entityName}Request request, CancellationToken cancellationToken)
    {
        var {entityName} = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = {entityName}
        ?? throw new NotFoundException(_t["{entityName} {0} Not Found.", request.Id]);

        {entityName}.Update(request.Name, request.Description);

        await _repository.UpdateAsync({entityName}, cancellationToken);

        return request.Id;
    }
}
