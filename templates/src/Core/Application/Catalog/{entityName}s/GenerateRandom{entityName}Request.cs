namespace FSH.Starter.Application.Catalog.{entityName}s;

public class GenerateRandom{entityName}Request : IRequest<string>
{
    public int NSeed { get; set; }
}

public class GenerateRandom{entityName}RequestHandler : IRequestHandler<GenerateRandom{entityName}Request, string>
{
    private readonly IJobService _jobService;

    public GenerateRandom{entityName}RequestHandler(IJobService jobService) => _jobService = jobService;

    public Task<string> Handle(GenerateRandom{entityName}Request request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Enqueue<I{entityName}GeneratorJob>(x => x.GenerateAsync(request.NSeed, default));
        return Task.FromResult(jobId);
    }
}
