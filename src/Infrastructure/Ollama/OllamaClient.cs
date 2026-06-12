using Application.Interfaces.Llm;
using Infrastructure.Ollama;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Infrastructure.Ollama;

public sealed class OllamaClient : ILlmClient
{
    private readonly HttpClient _http;
    private readonly OllamaOptions _options;

    public OllamaClient(
        HttpClient http,
        IOptions<OllamaOptions> options)
    {
        _http = http;
        _options = options.Value;
    }

    public async Task<string> PromptAsync(
        string prompt,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            model = _options.Model,
            prompt,
            stream = false
        };

        var response = await _http.PostAsJsonAsync(
            "/api/generate",
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromJsonAsync<OllamaResponse>(
            cancellationToken: cancellationToken);

        return json?.response ?? "";
    }

    private sealed class OllamaResponse
    {
        public string response { get; set; } = "";
    }
}
