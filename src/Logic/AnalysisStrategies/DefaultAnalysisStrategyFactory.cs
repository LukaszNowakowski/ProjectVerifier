namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.AnalysisStrategies;

using System;

using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.AnalysisStrategies.NoConnectedFiles;
using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.AnalysisStrategies.NotConnectedProjects;

using Microsoft.Extensions.DependencyInjection;

public class DefaultAnalysisStrategyFactory : IAnalysisStrategyFactory
{
    private readonly IServiceProvider serviceProvider;

    public DefaultAnalysisStrategyFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public IAnalysisStrategy? Create(WorkParameters parameters)
    {
        if (string.IsNullOrEmpty(parameters.ProjectPath))
        {
            return this.serviceProvider.GetService<NotConnectedProjectsStrategy>();
        }

        return this.serviceProvider.GetService<NotConnectedFilesStrategy>();
    }
}