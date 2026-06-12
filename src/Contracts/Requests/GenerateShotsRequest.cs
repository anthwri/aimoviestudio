namespace Contracts.Director;

public sealed class GenerateShotsRequest
{
    public Guid MovieId { get; set; }
    public Guid SceneId { get; set; }
}
