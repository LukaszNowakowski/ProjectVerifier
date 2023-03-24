namespace Logic.SolutionAnalyzer;

using System;

public class SolutionItem
{
    public SolutionItem(
        Guid id,
        string displayName,
        string relativePath,
        string typeName)
    {
        this.Id = id;
        this.DisplayName = displayName;
        this.RelativePath = relativePath;
        this.TypeName = typeName;
    }

    public Guid Id { get; }
    
    public string DisplayName { get; }
    
    public string RelativePath { get; }

    public string TypeName { get; }
}