namespace Logic;

using System.Threading;
using System.Threading.Tasks;

public interface IApplicationEngine
{
    Task RunAsync(WorkParameters parameters, CancellationToken cancellationToken);
}