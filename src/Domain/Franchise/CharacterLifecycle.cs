namespace Domain.Franchise;

public sealed class CharacterLifecycle
{
    public Guid CharacterId { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = "";

    public int Age { get; set; }

    public string CurrentArcState { get; set; } = ""introduction"";

    public List<string> MajorEvents { get; set; } = new();
}
