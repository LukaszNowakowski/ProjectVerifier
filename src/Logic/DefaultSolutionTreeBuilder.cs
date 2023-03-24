namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionAnalyzer;
using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionTreeBuilder;

public class DefaultSolutionTreeBuilder : ISolutionTreeBuilder
{
    public TreeNode BuildSolutionTree(IList<TreeItem> nesting, IList<SolutionItem> solutionItems)
    {
        var items = new Collection<NodeWithSolutionItem>();
        foreach (var item in nesting)
        {
            var solutionItem = solutionItems.First(s => s.Id == item.Id);
            items.Add(new() { SolutionItem = solutionItem, TreeItem = item });
        }

        var root = new TreeNode(new SolutionItem(Guid.Empty, "Solution", string.Empty, "Solution"),
            Enumerable.Empty<TreeNode>());
        var allNodes = new Collection<TreeNode>();
        var topLevelItems = solutionItems.Where(
            i => nesting.All(t => t.Id != i.Id))!;
        foreach (var tii in topLevelItems)
        {
            var node = new TreeNode(tii, Enumerable.Empty<TreeNode>());
            root.Children.Add(node);
            allNodes.Add(node);
        }

        foreach (var nestedItem in items)
        {
            var parent = allNodes.First(n => n.Item.Id == nestedItem.TreeItem.Id);
            var node = new TreeNode(nestedItem.SolutionItem, Enumerable.Empty<TreeNode>());
            parent!.Children.Add(node);
            allNodes.Add(node);
        }
        // var treeNodes = new Collection<TreeNode>();
        // for (var i = 0; i < nesting.Count; i++)
        // {
        //     var solutionItem = solutionItems.First(s => s.Id == nesting[i].Id);
        //     var node = new TreeNode(solutionItem, Enumerable.Empty<TreeNode>());
        //     treeNodes.Add(node);
        //     var parent = treeNodes.FirstOrDefault(tn => tn.Item.Id == nesting[i].ParentId);
        //     if (parent is not null)
        //     {
        //         parent.Children.Add(node);
        //     }
        //     else
        //     {
        //         Console.WriteLine("Added at root: {0}", node.Item.DisplayName);
        //         root.Children.Add(node);
        //     }
        // }

        // this.DisplayTreeNode(root, 0);

        return root;
    }

    private void DisplayTreeNode(TreeNode node, int level)
    {
        Console.WriteLine(
            "{0}{1}",
            new string(' ', level * 3),
            node.Item.DisplayName);
        foreach (var child in node.Children)
        {
            this.DisplayTreeNode(child, level + 1);
        }
    }

    private class NodeWithSolutionItem
    {
        public TreeItem TreeItem { get; set; }

        public SolutionItem SolutionItem { get; set; }
    }
}