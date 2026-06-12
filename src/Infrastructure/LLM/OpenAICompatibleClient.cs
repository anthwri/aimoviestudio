using System.Net.Http.Json;

namespace Infrastructure.LLM;

public sealed class OpenAICompatibleClient : ILLMClient
{
    private readonly HttpClient _http;
    private readonly string _model;

    public OpenAICompatibleClient(HttpClient http, string model = ""deepseek"")
    {
        _http = http;
        _model = model;
    }

    public async Task<string> GenerateAsync(string systemPrompt, string userPrompt)
    {
        var payload = new
        {
            model = _model,
            messages = new[]
            {
                new { role = ""system"", content = systemPrompt },
                new { role = ""user"", content = userPrompt }
            }
        };

        var response = await _http.PostAsJsonAsync(""/v1/chat/completions"", payload);
        var json = await response.Content.ReadAsStringAsync();

        return json;
    }
}
