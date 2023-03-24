namespace Logic;

using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

public class DefaultApplicationEngine : IApplicationEngine
{
    private readonly ILogger logger;

    public DefaultApplicationEngine(ILogger<DefaultApplicationEngine> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task RunAsync(WorkParameters parameters, CancellationToken cancellationToken)
    {
        this.logger.LogDebug("Start application execution.");
        await Task.Delay(5000, cancellationToken);
    }
}