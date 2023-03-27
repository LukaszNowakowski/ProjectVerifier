namespace AxaItSolutions.Tools.Migrations.ProjectVerifier.Logic;

using System;

public interface IProjectTypeTranslator
{
    string? GetProjectTypeName(Guid projectTypeId);
}