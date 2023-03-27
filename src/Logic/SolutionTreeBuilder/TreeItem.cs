namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionTreeBuilder;

using System;

public class TreeItem
{
    public TreeItem(Guid id, Guid parentId)
    {
        this.Id = id;
        this.ParentId = parentId;
    }
    
    public Guid Id { get; }

    public Guid ParentId { get; }
}