namespace Application.Interfaces.Llm;

public interface ILlmClient
{
    Task<string> PromptAsync(
        string prompt,
        CancellationToken cancellationToken = default);
}
