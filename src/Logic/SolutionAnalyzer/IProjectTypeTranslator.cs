namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic.SolutionAnalyzer;

using System;

public interface IProjectTypeTranslator
{
    string? GetProjectTypeName(Guid projectTypeId);
}