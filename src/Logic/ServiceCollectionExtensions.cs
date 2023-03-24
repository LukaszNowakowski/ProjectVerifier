namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLogic(
        this IServiceCollection services)
    {
        services
            .AddSingleton<IApplicationEngine, DefaultApplicationEngine>()
            .AddSingleton<ISolutionAnalyzer, DefaultSolutionAnalyzer>()
            .AddSingleton<IProjectTypeTranslator, DefaultProjectTypeTranslator>();
        return services;
    }
}