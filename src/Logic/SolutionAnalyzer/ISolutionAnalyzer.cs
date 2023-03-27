namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionAnalyzer;

using System.Collections.Generic;

public interface ISolutionAnalyzer
{
    IEnumerable<SolutionItem> FetchSolutionProjects(
        string solutionDirectory,
        string solutionFile);
}