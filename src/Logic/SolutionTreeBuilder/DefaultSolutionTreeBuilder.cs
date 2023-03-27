namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionAnalyzer;
using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionTreeBuilder;

public class DefaultSolutionTreeBuilder : ISolutionTreeBuilder
{
    public TreeNode BuildSolutionTree(IList<TreeItem> nesting, IList<SolutionItem> solutionItems)
    {
        var items = new Collection<NodeWithSolutionItem>();
        foreach (var solutionItem in solutionItems)
        {
            var structureElement = nesting.SingleOrDefault(s => s.Id == solutionItem.Id);
            items.Add(new() { SolutionItem = solutionItem, TreeItem = structureElement });
        }

        var allNodes = new Collection<TreeNode>();
        var root = new TreeNode(
            new SolutionItem(
                Guid.Empty,
                "Solution",
                string.Empty,
                "Solution"),
            Enumerable.Empty<TreeNode>());
        allNodes.Add(root);
        for (int i = items.Count - 1; i >= 0; i--)
        {
            var topLevelItem = items[i];
            if (topLevelItem.TreeItem is not null)
            {
                continue;
            }

            var treeNode = new TreeNode(
                topLevelItem.SolutionItem,
                Enumerable.Empty<TreeNode>());
            root.Children.Add(treeNode);
            allNodes.Add(treeNode);
            items.RemoveAt(i);
        }

        this.AddChildren(allNodes, items);
        return root;
    }

    private void AddChildren(
        IList<TreeNode> allNodes,
        IList<NodeWithSolutionItem> candidates)
    {
        var movedNodes = -1;
        while (candidates.Count > 0 && movedNodes != 0)
        {
            for (var i = 0; i < allNodes.Count; i++)
            {
                var treeNode = allNodes[i];
                var matchingCandidates = candidates
                    .Where(c => c.TreeItem is not null)
                    .Where(c => c.TreeItem!.ParentId == treeNode.Item.Id)
                    .ToList();
                foreach (var candidate in matchingCandidates)
                {
                    var newNode = new TreeNode(
                        candidate.SolutionItem,
                        Enumerable.Empty<TreeNode>());
                    treeNode.Children.Add(newNode);
                    allNodes.Add(newNode);
                    candidates.Remove(candidate);
                    movedNodes++;
                }
            }
        }
    }

    [DebuggerDisplay("{SolutionItem}")]
    private class NodeWithSolutionItem
    {
        public TreeItem? TreeItem { get; set; }

        public SolutionItem SolutionItem { get; set; } = default!;
    }
}