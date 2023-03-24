namespace Logic;

using System.Threading;
using System.Threading.Tasks;

public interface ISolutionAnalyzer
{
    Task AnalyzeSolutionAsync(
        string solutionDirectory,
        string solutionFile,
        CancellationToken cancellationToken);
}