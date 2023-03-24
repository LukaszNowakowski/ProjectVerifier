namespace AxaItSolutions.Tools.Migrations.ProjectVerifier;

using Logic;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IEnumerable<string> args)
    {
        services
            .AddLogic()
            .AddLogging();

        return services;
    }
}