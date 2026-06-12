namespace Contracts.Director;

public sealed class ShotResponse
{
    public int ShotNumber { get; set; }
    public string Type { get; set; } = "";
    public string CameraDescription { get; set; } = "";
    public string Prompt { get; set; } = "";
}
