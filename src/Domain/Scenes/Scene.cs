namespace Domain.Movies;

public partial class Scene
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid MovieId { get; set; }

    public int SceneNumber { get; set; }

    public string Summary { get; set; } = "";

    public List<Shot> Shots { get; set; } = new();
}
