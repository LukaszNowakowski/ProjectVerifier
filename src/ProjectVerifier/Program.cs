using AxaItSolutions.Tools.Migrations.ProjectVerifier;

using CommandLine;

using Logic;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddServices(args);
    })
    .Build();
var cancellationTokenSource = new CancellationTokenSource();
#pragma warning disable CS4014
Parser.Default.ParseArguments<RunOptions>(args)
    .WithParsedAsync<RunOptions>(async o =>
#pragma warning restore CS4014
    {
        await ExecuteAsync(host.Services, o)
            .ConfigureAwait(false);
        cancellationTokenSource.Cancel();
    });

await host.RunAsync(cancellationTokenSource.Token);

static Task ExecuteAsync(IServiceProvider hostProvider, RunOptions runOptions)
{
    try
    {
        using var serviceScope = hostProvider.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;
        var workParameters = new WorkParameters(
            runOptions.SolutionDirectory,
            runOptions.SolutionFile,
            runOptions.ProjectPath,
            runOptions.OutputPath);
        var applicationEngine = serviceProvider.GetRequiredService<IApplicationEngine>();
        return applicationEngine.RunAsync(workParameters, CancellationToken.None);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        throw;
    }
}