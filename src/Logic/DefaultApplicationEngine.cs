namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic;

using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

public class DefaultApplicationEngine : IApplicationEngine
{
    private readonly ILogger logger;

    private readonly ISolutionAnalyzer solutionAnalyzer;

    public DefaultApplicationEngine(
        ILogger<DefaultApplicationEngine> logger,
        ISolutionAnalyzer solutionAnalyzer)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.solutionAnalyzer = solutionAnalyzer ?? throw new ArgumentNullException(nameof(solutionAnalyzer));
    }

    public Task RunAsync(WorkParameters parameters, CancellationToken cancellationToken)
    {
        try
        {
            this.logger.LogDebug("Start application execution.");
            var projects = this.solutionAnalyzer.GetProjectsAsync(
                parameters.SolutionDirectory,
                parameters.SolutionFile);
            foreach (var project in projects)
            {
                this.logger.LogDebug("Found '{ProjectType}' named  '{ProjectName}'", project.TypeName, project.DisplayName);
            }

            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            throw;
        }
    }
}