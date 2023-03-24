namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionAnalyzer;

public interface ISolutionAnalyzer
{
    IAsyncEnumerable<SolutionItem> GetProjectsAsync(
        string solutionDirectory,
        string solutionFile,
        CancellationToken cancellationToken);
}