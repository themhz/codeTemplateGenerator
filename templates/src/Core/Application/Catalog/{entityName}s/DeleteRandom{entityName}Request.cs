namespace FSH.Starter.Application.Catalog.{entityName}s;

public class DeleteRandom{entityName}Request : IRequest<string>
{
}

public class DeleteRandom{entityName}RequestHandler : IRequestHandler<DeleteRandom{entityName}Request, string>
{
    private readonly IJobService _jobService;

    public DeleteRandom{entityName}RequestHandler(IJobService jobService) => _jobService = jobService;

    public Task<string> Handle(DeleteRandom{entityName}Request request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Schedule<I{entityName}GeneratorJob>(x => x.CleanAsync(default), TimeSpan.FromSeconds(5));
        return Task.FromResult(jobId);
    }
}
