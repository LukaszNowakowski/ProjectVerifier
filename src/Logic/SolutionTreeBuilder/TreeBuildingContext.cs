namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionTreeBuilder;

public class TreeBuildingContext
{
    public TreeBuildingContext(
        TreeItem root)
    {
        this.Root = root;
    }
    
    public TreeItem Root { get; }
}