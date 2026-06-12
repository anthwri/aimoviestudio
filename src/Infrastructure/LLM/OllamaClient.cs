using System.Net.Http.Json;

namespace Infrastructure.LLM;

public sealed class OllamaClient : ILLMClient
{
    private readonly HttpClient _http;

    public OllamaClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> GenerateAsync(
        string system,
        string prompt)
    {
        var response =
            await _http.PostAsJsonAsync(
                "http://localhost:11434/api/generate",
                new
                {
                    model = "qwen3:latest",
                    prompt = $"{system}\n\n{prompt}",
                    stream = false
                });

        response.EnsureSuccessStatusCode();

        var json =
            await response.Content.ReadFromJsonAsync<OllamaResponse>();

        return json?.response ?? "";
    }

    private sealed class OllamaResponse
    {
        public string response { get; set; } = "";
    }
}
