namespace Domain.Movies;

public sealed class LoraTrainingJob
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string CharacterName { get; set; } = "";

    public string DatasetPath { get; set; } = "";

    public int Epochs { get; set; } = 10;

    public string BaseModel { get; set; } = ""sdxl-base-1.0"";

    public string OutputPath { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
