namespace FSH.Starter.Application.Catalog.Bananas;

public class GenerateRandomBananaRequest : IRequest<string>
{
    public int NSeed { get; set; }
}

public class GenerateRandomBananaRequestHandler : IRequestHandler<GenerateRandomBananaRequest, string>
{
    private readonly IJobService _jobService;

    public GenerateRandomBananaRequestHandler(IJobService jobService) => _jobService = jobService;

    public Task<string> Handle(GenerateRandomBananaRequest request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Enqueue<IBananaGeneratorJob>(x => x.GenerateAsync(request.NSeed, default));
        return Task.FromResult(jobId);
    }
}
