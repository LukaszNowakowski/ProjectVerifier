namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.AnalysisStrategies;

public interface IAnalysisStrategyFactory
{
    IAnalysisStrategy? Create(WorkParameters parameters);
}