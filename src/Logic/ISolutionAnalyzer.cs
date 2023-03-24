namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic;

using System.Collections.Generic;
using System.Threading;

using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionAnalyzer;

public interface ISolutionAnalyzer
{
    IEnumerable<SolutionItem> GetProjectsAsync(
        string solutionDirectory,
        string solutionFile);
}