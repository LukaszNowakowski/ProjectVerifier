namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.AnalysisStrategies.NotConnectedProjects;

using System.Collections.Generic;

using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionAnalyzer;

public interface ISolutionVerifier
{
    IEnumerable<string> FindProjectsNotInSolution(string solutionDirectory, IEnumerable<SolutionItem> projects);
}
