namespace Logic.SolutionAnalyzer;

public class Solution
{
    public Solution(string directory, string fileName)
    {
        this.Directory = directory;
        this.FileName = fileName;
    }

    public string Directory { get; }

    public string FileName { get; }
}