namespace Domain.Consistency;

public sealed class WardrobeProfile
{
    public string CharacterId { get; set; } = "";

    public string OutfitId { get; set; } = "";

    public string Description { get; set; } = "";

    public bool IsCurrentOutfit { get; set; }
}
