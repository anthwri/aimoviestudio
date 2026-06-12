namespace Domain.Consistency;

public sealed class AppearanceLock
{
    public string CharacterId { get; set; } = "";

    public string HairStyle { get; set; } = "";

    public string HairColor { get; set; } = "";

    public string EyeColor { get; set; } = "";

    public string FaceDescription { get; set; } = "";

    public string BodyDescription { get; set; } = "";

    public bool Locked { get; set; } = true;
}
