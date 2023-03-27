namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.AnalysisStrategies.NoConnectedFiles;

using System.Collections.Generic;

public interface IProjectVerifier
{
    IEnumerable<string> GetNotConnectedFiles(string solutionDirectory, string projectRelativePath);
}