namespace FSH.Starter.Application.Catalog.Bananas;

public class DeleteRandomBananaRequest : IRequest<string>
{
}

public class DeleteRandomBananaRequestHandler : IRequestHandler<DeleteRandomBananaRequest, string>
{
    private readonly IJobService _jobService;

    public DeleteRandomBananaRequestHandler(IJobService jobService) => _jobService = jobService;

    public Task<string> Handle(DeleteRandomBananaRequest request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Schedule<IBananaGeneratorJob>(x => x.CleanAsync(default), TimeSpan.FromSeconds(5));
        return Task.FromResult(jobId);
    }
}
