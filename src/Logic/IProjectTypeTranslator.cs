namespace Logic;

using System;

public interface IProjectTypeTranslator
{
    string? GetProjectTypeName(Guid projectTypeId);
}