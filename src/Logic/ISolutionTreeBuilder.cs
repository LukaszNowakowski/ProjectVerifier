namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic;

using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionTreeBuilder;

public interface ISolutionTreeBuilder
{
    TreeBuildingContext StartBuild();

    void AddItem(TreeItem item);
}