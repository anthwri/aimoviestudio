namespace Contracts.Director;

public sealed class CreateFilmRequest
{
    public string Idea { get; set; } = "";

    public int MaxScenes { get; set; } = 5;

    public bool GenerateImages { get; set; } = true;
}
