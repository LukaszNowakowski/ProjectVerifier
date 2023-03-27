namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.AnalysisStrategies.NotConnectedProjects;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionAnalyzer;

public class DefaultSolutionVerifier : ISolutionVerifier
{
    private readonly IFileSystem fileSystem;

    public DefaultSolutionVerifier(
        IFileSystem fileSystem)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }
    
    public IEnumerable<string> FindProjectsNotInSolution(string solutionDirectory, IEnumerable<SolutionItem> projects)
    {
        return this.FetchExistingProjectFiles(solutionDirectory)
            .Where(project => !projects.Any(p =>
                this.fileSystem.Path.Join(
                        solutionDirectory,
                        p.RelativePath)
                    .Equals(
                        project,
                        StringComparison.InvariantCultureIgnoreCase)));
    }

    private IEnumerable<string> FetchExistingProjectFiles(string solutionDirectory)
    {
        return this.fileSystem.Directory.EnumerateFiles(
            solutionDirectory,
            "*.csproj",
            SearchOption.AllDirectories);
    }
}