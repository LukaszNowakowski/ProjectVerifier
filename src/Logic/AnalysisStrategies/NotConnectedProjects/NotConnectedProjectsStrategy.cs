namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.AnalysisStrategies.NotConnectedProjects;

using System;
using System.Collections.Generic;

using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionAnalyzer;

public class NotConnectedProjectsStrategy : IAnalysisStrategy
{
    private readonly ISolutionVerifier solutionVerifier;

    public NotConnectedProjectsStrategy(ISolutionVerifier solutionVerifier)
    {
        this.solutionVerifier = solutionVerifier ?? throw new ArgumentNullException(nameof(solutionVerifier));
    }

    public void RunAnalysis(WorkParameters parameters, IEnumerable<SolutionItem> projects)
    {
        var invalidItems = this.solutionVerifier.FindProjectsNotInSolution(
            parameters.SolutionDirectory,
            projects);

        foreach (var project in invalidItems)
        {
            Console.WriteLine(project);
        }
    }
}