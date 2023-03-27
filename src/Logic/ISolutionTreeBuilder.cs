namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic;

using System.Collections.Generic;

using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionAnalyzer;
using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionTreeBuilder;

public interface ISolutionTreeBuilder
{
    TreeNode BuildSolutionTree(IList<TreeItem> nesting, IList<SolutionItem> solutionItems);
}