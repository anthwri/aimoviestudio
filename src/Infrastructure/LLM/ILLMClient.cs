namespace Infrastructure.LLM;

public interface ILLMClient
{
    Task<string> GenerateAsync(string systemPrompt, string userPrompt);
}
