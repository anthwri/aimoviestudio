using System.Net.Http.Json;

namespace Infrastructure.Vector;

public sealed class QdrantClient
{
    private readonly HttpClient _http;

    public QdrantClient(HttpClient http)
    {
        _http = http;
    }

    public async Task UpsertAsync(string collection, string id, float[] vector)
    {
        var payload = new
        {
            points = new[]
            {
                new
                {
                    id = id,
                    vector = vector
                }
            }
        };

        await _http.PutAsJsonAsync(
            $""http://localhost:6333/collections/{collection}/points"",
            payload);
    }

    public async Task<string[]> SearchAsync(
        string collection,
        float[] vector,
        int limit = 5)
    {
        var response =
            await _http.PostAsJsonAsync(
                $""http://localhost:6333/collections/{collection}/points/search"",
                new
                {
                    vector = vector,
                    limit = limit
                });

        var result =
            await response.Content.ReadAsStringAsync();

        return new[] { result };
    }
}
