namespace Contracts.Director;

public sealed class MovieResponse
{
    public string Title { get; set; } = "";
    public string Logline { get; set; } = "";

    public List<CharacterDto> Characters { get; set; } = new();
    public List<LocationDto> Locations { get; set; } = new();
    public List<PropDto> Props { get; set; } = new();
    public List<SceneDto> Scenes { get; set; } = new();
}

public sealed class CharacterDto
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Personality { get; set; } = "";
    public string Wardrobe { get; set; } = "";
}

public sealed class LocationDto
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
}

public sealed class PropDto
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
}

public sealed class SceneDto
{
    public int SceneNumber { get; set; }
    public string Summary { get; set; } = "";
}
