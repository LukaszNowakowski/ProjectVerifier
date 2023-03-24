namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic;

using System.Collections.Generic;
using System.Threading;

using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionAnalyzer;
using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionTreeBuilder;

public interface ISolutionAnalyzer
{
    TreeNode GetProjectsAsync(
        string solutionDirectory,
        string solutionFile);
}