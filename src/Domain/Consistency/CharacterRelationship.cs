namespace Domain.Consistency;

public sealed class CharacterRelationship
{
    public string CharacterA { get; set; } = "";

    public string CharacterB { get; set; } = "";

    public string RelationshipType { get; set; } = "";

    public int TrustLevel { get; set; }
}
