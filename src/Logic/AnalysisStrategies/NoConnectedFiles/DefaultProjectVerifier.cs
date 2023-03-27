namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.AnalysisStrategies.NoConnectedFiles;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

public class DefaultProjectVerifier : IProjectVerifier
{
    private static readonly ReadOnlyCollection<string> ExcludedElements = new(
        new[]
        {
            "Reference",
            "ProjectReference",
            "Folder",
        });

    private readonly IFileSystem fileSystem;

    public DefaultProjectVerifier(
        IFileSystem fileSystem)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    public IEnumerable<string> GetNotConnectedFiles(string solutionDirectory, string projectRelativePath)
    {
        var projectFilePath = this.fileSystem.Path.Join(solutionDirectory, projectRelativePath);
        var projectDirectory = this.fileSystem.Path.GetDirectoryName(projectFilePath);
        var includedFiles = this.GetIncludedFiles(projectFilePath, projectDirectory!)
            .ToList();
        var existingFiles = this.GetExistingFiles(projectDirectory!);
        foreach (var file in existingFiles)
        {
            if (includedFiles.Any(f => f.Equals(file, StringComparison.InvariantCultureIgnoreCase)))
            {
                continue;
            }

            Console.WriteLine(file);
        }
        
        yield break;
    }

    private IEnumerable<string> GetIncludedFiles(string projectFilePath, string projectDirectory)
    {
        var reader = new XmlTextReader(projectFilePath);
        var document = XDocument.Load(reader);
        var table = reader!.NameTable;
        var namespaceManager = new XmlNamespaceManager(table!);
        namespaceManager.AddNamespace("vs", "http://schemas.microsoft.com/developer/msbuild/2003");
        var files = (IEnumerable)document.Document!.XPathEvaluate("/vs:Project/vs:ItemGroup/*/@Include ",
            namespaceManager);
        foreach (var file in files.OfType<XAttribute>())
        {
            if (ExcludedElements.Contains(file.Parent!.Name.LocalName))
            {
                continue;
            }

            yield return this.fileSystem.Path.Join(projectDirectory, file.Value);
        }
    }

    private IEnumerable<string> GetExistingFiles(string projectDirectory)
    {
        return this.fileSystem.Directory
            .GetFiles(
                projectDirectory,
                "*.*",
                SearchOption.AllDirectories)
            .Where(f => !this.fileSystem.Path!.GetDirectoryName(f)!.Contains("\\bin\\") && !this.fileSystem.Path!.GetDirectoryName(f)!.EndsWith("\\bin"))
            .Where(f => !this.fileSystem.Path!.GetDirectoryName(f)!.Contains("\\obj\\") && !this.fileSystem.Path!.GetDirectoryName(f)!.EndsWith("\\obj"))
            .Where(f => !this.fileSystem.Path!.GetFileName(f).Equals("project.lock.json", StringComparison.InvariantCultureIgnoreCase))
            .Where(f => !this.fileSystem.Path!.GetFileName(f).Contains(".csproj"))
            .Where(f => !this.fileSystem.Path!.GetFileName(f).Equals("stylecop.cache", StringComparison.InvariantCultureIgnoreCase));
    }
}