namespace Domain.Agents;

public abstract class BaseAgent
{
    public string Name { get; set; } = "";
    public string SystemPrompt { get; set; } = "";
}
