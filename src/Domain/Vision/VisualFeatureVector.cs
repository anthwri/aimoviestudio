namespace Domain.Vision;

public sealed class VisualFeatureVector
{
    public string AssetId { get; set; } = "";

    public float[] Embedding { get; set; }
        = Array.Empty<float>();

    public string Type { get; set; } = "";

    public DateTime GeneratedUtc { get; set; }
}
