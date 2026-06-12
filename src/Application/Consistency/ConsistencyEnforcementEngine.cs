namespace Application.Consistency;

public sealed class ConsistencyEnforcementEngine
{
    public string BuildPrompt(
        string scenePrompt,
        string characterLock,
        string environmentLock)
    {
        return $"""
CONSISTENCY RULES

{characterLock}

{environmentLock}

SCENE REQUEST

{scenePrompt}
""";
    }
}
