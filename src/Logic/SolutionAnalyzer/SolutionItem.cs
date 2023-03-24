namespace Logic.SolutionAnalyzer;

public class SolutionItem
{
    public SolutionItem(
        string relativePath)
    {
        this.RelativePath = relativePath;
    }
    
    public string RelativePath { get; }
}