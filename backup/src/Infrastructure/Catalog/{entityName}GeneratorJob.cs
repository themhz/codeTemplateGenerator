using Ardalis.Specification;
using FSH.Starter.Application.Catalog.Brands;
using FSH.Starter.Application.Common.Interfaces;
using FSH.Starter.Application.Common.Persistence;
using FSH.Starter.Domain.Catalog;
using FSH.Starter.Shared.Notifications;
using Hangfire;
using Hangfire.Console.Extensions;
using Hangfire.Console.Progress;
using Hangfire.Server;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.Infrastructure.Catalog;

public class {entityName}GeneratorJob : IBrandGeneratorJob
{
    private readonly ILogger<{entityName}GeneratorJob> _logger;
    private readonly ISender _mediator;
    private readonly IReadRepository<{entityName}> _repository;
    private readonly IProgressBarFactory _progressBar;
    private readonly PerformingContext _performingContext;
    private readonly INotificationSender _notifications;
    private readonly ICurrentUser _currentUser;
    private readonly IProgressBar _progress;

    public {entityName}GeneratorJob(
        ILogger<{entityName}GeneratorJob> logger,
        ISender mediator,
        IReadRepository<{entityName}> repository,
        IProgressBarFactory progressBar,
        PerformingContext performingContext,
        INotificationSender notifications,
        ICurrentUser currentUser)
    {
        _logger = logger;
        _mediator = mediator;
        _repository = repository;
        _progressBar = progressBar;
        _performingContext = performingContext;
        _notifications = notifications;
        _currentUser = currentUser;
        _progress = _progressBar.Create();
    }

    private async Task NotifyAsync(string message, int progress, CancellationToken cancellationToken)
    {
        _progress.SetValue(progress);
        await _notifications.SendToUserAsync(
            new JobNotification()
            {
                JobId = _performingContext.BackgroundJob.Id,
                Message = message,
                Progress = progress
            },
            _currentUser.GetUserId().ToString(),
            cancellationToken);
    }

    [Queue("notdefault")]
    public async Task GenerateAsync(int nSeed, CancellationToken cancellationToken)
    {
        await NotifyAsync("Your job processing has started", 0, cancellationToken);

        foreach (int index in Enumerable.Range(1, nSeed))
        {
            await _mediator.Send(
                new Create{entityName}Request
                {
                    Name = $"{entityName} Random - {Guid.NewGuid()}",
                    Description = "Funny description"
                },
                cancellationToken);

            await NotifyAsync("Progress: ", nSeed > 0 ? (index * 100 / nSeed) : 0, cancellationToken);
        }

        await NotifyAsync("Job successfully completed", 0, cancellationToken);
    }

    [Queue("notdefault")]
    [AutomaticRetry(Attempts = 5)]
    public async Task CleanAsync(CancellationToken cancellationToken)
    {
      /*  _logger.LogInformation("Initializing {entityName} with Id: {jobId}", _performingContext.BackgroundJob.Id);

        var items = await _repository.ListAsync(new Random{entityName}sSpec(), cancellationToken);

        _logger.LogInformation("{entityName}s Random: {{entityName}sCount} ", items.Count.ToString());

        foreach (var item in items)
        {
            await _mediator.Send(new DeleteBrandRequest(item.Id), cancellationToken);
        }

        _logger.LogInformation("All random {entityName}s deleted.");*/
    }
}

public class Random{entityName}sSpec : Specification<{entityName}>
{
    public Random{entityName}sSpec() =>
        Query.Where(b => !string.IsNullOrEmpty(b.Name) && b.Name.Contains("{entityName} Random"));
}
