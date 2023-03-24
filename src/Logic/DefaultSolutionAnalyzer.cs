namespace Logic;

using System.Threading;
using System.Threading.Tasks;

public class DefaultSolutionAnalyzer : ISolutionAnalyzer
{
    public Task AnalyzeSolutionAsync(string solutionDirectory, string solutionFile, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}