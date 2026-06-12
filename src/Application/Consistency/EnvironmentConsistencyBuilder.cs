using Domain.Consistency;

namespace Application.Consistency;

public sealed class EnvironmentConsistencyBuilder
{
    public string Build(
        LocationLayoutLock environment)
    {
        return $"""
LOCATION LOCK

Layout:
{environment.LayoutDescription}

Lighting:
{environment.LightingDescription}

Permanent Objects:
{string.Join(",", environment.PermanentObjects)}

DO NOT REMOVE OR ALTER
""";
    }
}
