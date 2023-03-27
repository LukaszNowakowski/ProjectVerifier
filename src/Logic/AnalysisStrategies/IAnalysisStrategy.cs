namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.AnalysisStrategies;

using System.Collections.Generic;

using AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionAnalyzer;

public interface IAnalysisStrategy
{
    void RunAnalysis(WorkParameters parameters, IEnumerable<SolutionItem> projects);
}