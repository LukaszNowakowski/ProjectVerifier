namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.AnalysisStrategies;
using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.AnalysisStrategies.NotConnectedProjects;
using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionAnalyzer;

using Microsoft.Extensions.Logging;

public class DefaultApplicationEngine : IApplicationEngine
{
    private readonly ILogger logger;

    private readonly ISolutionAnalyzer solutionAnalyzer;

    private readonly IAnalysisStrategyFactory analysisStrategyFactory;

    public DefaultApplicationEngine(
        ILogger<DefaultApplicationEngine> logger,
        ISolutionAnalyzer solutionAnalyzer,
        IAnalysisStrategyFactory analysisStrategyFactory)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.solutionAnalyzer = solutionAnalyzer ?? throw new ArgumentNullException(nameof(solutionAnalyzer));
        this.analysisStrategyFactory = analysisStrategyFactory ?? throw new ArgumentNullException(nameof(analysisStrategyFactory));
    }

    public Task RunAsync(WorkParameters parameters, CancellationToken cancellationToken)
    {
        try
        {
            this.logger.LogDebug("Start application execution.");
            var projects = this.solutionAnalyzer.FetchSolutionProjects(
                parameters.SolutionDirectory,
                parameters.SolutionFile);

            var strategy = this.analysisStrategyFactory.Create(parameters);
            if (strategy is null)
            {
                this.logger.LogWarning("No strategies found for given parameters");
            }
            else
            {
                strategy.RunAnalysis(parameters, projects);
            }

            Console.WriteLine("COMPLETED");
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