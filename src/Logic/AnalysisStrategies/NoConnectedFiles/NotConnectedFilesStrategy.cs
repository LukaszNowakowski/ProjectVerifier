namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.AnalysisStrategies.NoConnectedFiles;

using System;
using System.Collections.Generic;
using System.Linq;

using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionAnalyzer;

using Microsoft.Extensions.Logging;

public class NotConnectedFilesStrategy : IAnalysisStrategy
{
    private readonly IProjectVerifier projectVerifier;

    private readonly ILogger logger;

    public NotConnectedFilesStrategy(
        IProjectVerifier projectVerifier,
        ILogger<NotConnectedFilesStrategy> logger)
    {
        this.projectVerifier = projectVerifier ?? throw new ArgumentNullException(nameof(projectVerifier));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void RunAnalysis(WorkParameters parameters, IEnumerable<SolutionItem> projects)
    {
        var selectedProject = projects.SingleOrDefault(
            p => p.RelativePath.Equals(parameters.ProjectPath, StringComparison.InvariantCultureIgnoreCase));
        if (selectedProject is null)
        {
            this.logger.LogWarning(
                "Project file with relative path '{ProjectPath}' was not found",
                parameters.ProjectPath);
            return;
        }

        var invalidItems = this.projectVerifier.GetNotConnectedFiles(
            parameters.SolutionDirectory,
            selectedProject.RelativePath);
        
        foreach (var file in invalidItems)
        {
            Console.WriteLine(file);
        }
    }
}
