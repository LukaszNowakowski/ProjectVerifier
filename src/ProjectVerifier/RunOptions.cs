namespace AxaItSolutions.Tools.Migrations.ProjectVerifier;

using CommandLine;

public class RunOptions
{
    [Option(
        "solutionDirectory",
        Required = true,
        HelpText = "Path to directory containing solution to be analyzed")]
    public string SolutionDirectory { get; set; } = default!;

    [Option(
        "solutionFile",
        Required = true,
        HelpText = "Name of solution file to be analyzed (with extension)")]
    public string SolutionFile { get; set; } = default!;

    [Option(
        "projectPath",
        Required = false,
        HelpText = "Project path (relative to solution directory) to be analyzed")]
    public string? ProjectPath { get; set; }

    [Option(
        "outputPath",
        Required = false,
        HelpText = "File to output results to")]
    public string? OutputPath { get; set; }

    public void CopyTo(RunOptions other)
    {
        other.SolutionDirectory = this.SolutionDirectory;
        other.SolutionFile = this.SolutionFile;
        other.ProjectPath = this.ProjectPath;
        other.OutputPath = this.OutputPath;
    }
}