namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionAnalyzer;

using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text.RegularExpressions;

using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionTreeBuilder;

public class DefaultSolutionAnalyzer : ISolutionAnalyzer
{
    private static readonly Regex ProjectInformationParser = new Regex(
        "Project\\(\\\"\\{([A-Z0-9]{8}\\-[A-Z0-9]{4}\\-[A-Z0-9]{4}\\-[A-Z0-9]{4}\\-[A-Z0-9]{12})\\}\\\"\\) = \\\"([\\w\\ \\.]*)\\\", \\\"([\\w\\ \\.\\\\]*)\\\", \\\"\\{([A-Z0-9]{8}\\-[A-Z0-9]{4}\\-[A-Z0-9]{4}\\-[A-Z0-9]{4}\\-[A-Z0-9]{12})\\}\\\"",
        RegexOptions.Compiled);

    private static readonly Regex GuidParser = new Regex(
        @"\w{8}\-\w{4}\-\w{4}-\w{4}-\w{12}",
        RegexOptions.Compiled);

    private readonly IFileSystem fileSystem;

    private readonly IProjectTypeTranslator projectTypeTranslator;

    private readonly ISolutionTreeBuilder solutionTreeBuilder;

    public DefaultSolutionAnalyzer(
        IFileSystem fileSystem,
        IProjectTypeTranslator projectTypeTranslator,
        ISolutionTreeBuilder solutionTreeBuilder)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.projectTypeTranslator = projectTypeTranslator ?? throw new ArgumentNullException(nameof(projectTypeTranslator));
        this.solutionTreeBuilder = solutionTreeBuilder ?? throw new ArgumentNullException(nameof(solutionTreeBuilder));
    }

    public TreeNode BuildProjectsTreeAsync(
        string solutionDirectory,
        string solutionFile)
    {
        var solution = new Solution(solutionDirectory, solutionFile);
        var fullSolutionPath = this.fileSystem.Path.Combine(solution.Directory, solution.FileName);
        using var solutionFileLinesEnumerator = this.fileSystem.File.ReadLines(fullSolutionPath)
            .GetEnumerator();
        solutionFileLinesEnumerator.MoveNext();
        this.SkipHeaderLines(solutionFileLinesEnumerator);

        var solutionItems = this.ReadSolutionItems(solutionFileLinesEnumerator)
            .ToList();
        var nesting = this.ReadNesting(solutionFileLinesEnumerator)
            .ToList();
        var tree = this.solutionTreeBuilder.BuildSolutionTree(nesting, solutionItems);
        return tree;
    }
    
    private IEnumerable<SolutionItem> ReadSolutionItems(IEnumerator<string> solutionFile)
    {
        while (this.IsProjectStart(solutionFile.Current!))
        {
            var project = this.ReadProject(solutionFile);
            var typeName = this.projectTypeTranslator.GetProjectTypeName(project.ProjectTypeGuid) ?? "---";
            yield return new SolutionItem(project.ProjectId, project.Name, project.Path, typeName);
        }        
    }

    private void SkipHeaderLines(IEnumerator<string> enumerator)
    {
        if (enumerator.Current is null)
        {
            return;
        }

        while (!this.IsProjectStart(enumerator.Current!))
        {
            enumerator.MoveNext();
        }
    }

    private bool IsProjectStart(string contents)
    {
        return contents.StartsWith("project", StringComparison.InvariantCultureIgnoreCase);
    }

    private bool IsNestingStart(string contents)
    {
        return contents.Contains("globalsection(nestedprojects)", StringComparison.InvariantCultureIgnoreCase);
    }

    private bool IsGlobalSectionEnd(string contents)
    {
        return contents.Contains("endglobalsection", StringComparison.InvariantCultureIgnoreCase);
    }

    private ProjectEntry ReadProject(IEnumerator<string> solutionFileEnumerator)
    {
        if (!this.IsProjectStart(solutionFileEnumerator.Current!))
        {
            throw new InvalidOperationException($"'{solutionFileEnumerator.Current}' is not project line");
        }

        var matches = ProjectInformationParser.Matches(solutionFileEnumerator.Current!);
        if (matches.Count != 1)
        {
            throw new InvalidOperationException(
                $"Unable to read project data correctly for '{solutionFileEnumerator.Current}'");
        }

        var projectDataMatch = matches[0];
        var projectTypeId = Guid.Parse(projectDataMatch.Groups[1].Value);
        var name = projectDataMatch.Groups[2].Value;
        var path = projectDataMatch.Groups[3].Value;
        var projectId = Guid.Parse(projectDataMatch.Groups[4].Value);

        do
        {
            solutionFileEnumerator.MoveNext();
        } while (!solutionFileEnumerator.Current!.StartsWith("endproject",
                     StringComparison.InvariantCultureIgnoreCase));

        solutionFileEnumerator.MoveNext();
        return new ProjectEntry(projectTypeId, name, path, projectId);
    }

    private IEnumerable<TreeItem> ReadNesting(IEnumerator<string> solutionFile)
    {
        if (!solutionFile.Current!.Equals("global", StringComparison.InvariantCultureIgnoreCase))
        {
            throw new InvalidOperationException(
                $"Expected 'Global' section of solution file, '{solutionFile.Current}' found");
        }

        while (!this.IsNestingStart(solutionFile.Current!))
        {
            solutionFile.MoveNext();
        }

        solutionFile.MoveNext();
        while (!this.IsGlobalSectionEnd(solutionFile.Current!))
        {
            yield return this.ReadTreeItem(solutionFile.Current!);
            solutionFile.MoveNext();
        }
    }

    private TreeItem ReadTreeItem(string content)
    {
        var matches = GuidParser.Matches(content);
        var child = Guid.Parse(matches[0].Value);
        var parent = Guid.Parse(matches[1].Value);
        return new TreeItem(child, parent);
    }

    private class ProjectEntry
    {
        public ProjectEntry(
            Guid projectTypeId,
            string name,
            string path,
            Guid projectId)
        {
            this.ProjectTypeGuid = projectTypeId;
            this.Name = name;
            this.Path = path;
            this.ProjectId = projectId;
        }

        public Guid ProjectTypeGuid { get; }

        public string Name { get; }

        public string Path { get; }

        public Guid ProjectId { get; }
    }
}