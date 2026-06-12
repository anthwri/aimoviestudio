namespace Contracts.Director;

public sealed class GenerateStoryboardRequest
{
    public Guid MovieId { get; set; }

    public Guid SceneId { get; set; }
}
