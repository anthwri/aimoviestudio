namespace Contracts.Director;

public sealed class FilmResponse
{
    public string Title { get; set; } = "";

    public Guid MovieId { get; set; }

    public List<SceneResult> Scenes { get; set; } = new();
}

public sealed class SceneResult
{
    public int SceneNumber { get; set; }

    public List<ShotResult> Shots { get; set; } = new();
}

public sealed class ShotResult
{
    public int ShotNumber { get; set; }

    public string Prompt { get; set; } = "";

    public string? ImagePath { get; set; }
}
