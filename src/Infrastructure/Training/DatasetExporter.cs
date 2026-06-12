using System.IO;
using Domain.Movies;

namespace Infrastructure.Training;

public sealed class DatasetExporter
{
    public string Export(CharacterIdentity character, List<string> images)
    {
        var basePath = Path.Combine("training_data", character.Name);
        Directory.CreateDirectory(basePath);

        var captionsPath = Path.Combine(basePath, "captions.txt");

        using var writer = new StreamWriter(captionsPath);

        int i = 0;
        foreach (var img in images)
        {
            var fileName = $"img_{i}.png";
            var dest = Path.Combine(basePath, fileName);

            File.Copy(img, dest, overwrite: true);

            writer.WriteLine($"{fileName}|{character.Name}, consistent face, cinematic portrait");

            i++;
        }

        return basePath;
    }
}
