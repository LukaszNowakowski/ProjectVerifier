namespace Logic;

public class WorkParameters
{
    public WorkParameters(string solutionDirectory, string solutionFile, string? projectPath, string? outputPath)
    {
        this.SolutionDirectory = solutionDirectory;
        this.SolutionFile = solutionFile;
        this.ProjectPath = projectPath;
        this.OutputPath = outputPath;
    }

    public string SolutionDirectory { get; }

    public string SolutionFile { get; }

    public string? ProjectPath { get; }

    public string? OutputPath { get; }
}