namespace Domain.Movies;

public sealed class Movie
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = "";
    public string Logline { get; set; } = "";

    public List<Character> Characters { get; set; } = new();
    public List<Location> Locations { get; set; } = new();
    public List<Prop> Props { get; set; } = new();
    public List<Scene> Scenes { get; set; } = new();
}
using System.Collections.Generic;

namespace Domain.Movies;

public partial class Movie
{
    public List<Shot> Shots { get; set; } = new();
}
