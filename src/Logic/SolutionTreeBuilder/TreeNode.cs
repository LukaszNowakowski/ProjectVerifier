namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionTreeBuilder;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionAnalyzer;

public class TreeNode
{
    public TreeNode(
        SolutionItem item,
        IEnumerable<TreeNode> children)
    {
        this.Item = item;
        this.Children = new Collection<TreeNode>(children.ToList());
    }
    
    public SolutionItem Item { get; }

    public Collection<TreeNode> Children { get; }
}