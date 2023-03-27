namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic;

using System;
using System.Threading;
using System.Threading.Tasks;

using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionAnalyzer;
using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionTreeBuilder;

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
            var treeRoot = this.solutionAnalyzer.BuildProjectsTreeAsync(
                parameters.SolutionDirectory,
                parameters.SolutionFile);
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