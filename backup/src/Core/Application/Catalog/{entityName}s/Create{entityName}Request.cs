namespace FSH.Starter.Application.Catalog.{entityName}s;

public class Create{entityName}Request : IRequest<Guid>
{
    public string? Name { get; set; } = default!;
    public string? Description { get; set; }
}

public class Create{entityName}RequestValidator : CustomValidator<Create{entityName}Request>
{
    public Create{entityName}RequestValidator(IReadRepository<{entityName}> repository, IStringLocalizer<Create{entityName}RequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.FirstOrDefaultAsync(new {entityName}ByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["{entityName} {0} already Exists.", name]);
}

public class Create{entityName}RequestHandler : IRequestHandler<Create{entityName}Request, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<{entityName}> _repository;

    public Create{entityName}RequestHandler(IRepositoryWithEvents<{entityName}> repository) => _repository = repository;

    public async Task<Guid> Handle(Create{entityName}Request request, CancellationToken cancellationToken)
    {
        var {entityName} = new {entityName}(request.Name, request.Description);

        await _repository.AddAsync({entityName}, cancellationToken);

        return {entityName}.Id;
    }
}
