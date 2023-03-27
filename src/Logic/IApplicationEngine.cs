namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic;

using System.Threading;
using System.Threading.Tasks;

public interface IApplicationEngine
{
    Task RunAsync(WorkParameters parameters, CancellationToken cancellationToken);
}