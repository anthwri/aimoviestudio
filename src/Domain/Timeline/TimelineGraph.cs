namespace Domain.Timeline;

public sealed class FrameNode
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int FrameIndex { get; set; }
    public string ImagePath { get; set; } = "";
}

public sealed class ClipNode
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public List<FrameNode> Frames { get; set; } = new();
    public string Description { get; set; } = "";
}

public sealed class SceneSequence
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public List<ClipNode> Clips { get; set; } = new();
    public string SceneName { get; set; } = "";
}
