namespace Logic;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Logic.SolutionAnalyzer;

public interface ISolutionAnalyzer
{
    IAsyncEnumerable<SolutionItem> GetProjectsAsync(
        string solutionDirectory,
        string solutionFile,
        CancellationToken cancellationToken);
}