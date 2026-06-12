using Domain.Movies;
using Contracts.Director;

namespace Agents.Storyboard;

public sealed class StoryboardAgent
{
    public StoryboardFrameResponse Generate(Movie movie, Scene scene, Shot shot)
    {
        var prompt = $@"
You are a cinematic storyboard artist.

Create a highly detailed AI image prompt for this shot.

Movie:
{movie.Title}

Scene:
{scene.Summary}

Shot:
{shot.CameraDescription}

Return structured output:

- prompt (detailed cinematic image prompt)
- negative prompt (what to avoid)
- style (cinematic style)
- camera (lens + angle)
- lighting
- composition

Rules:
- Maintain consistent character appearance
- Maintain consistent environment
- Do NOT change wardrobe or location
- Make it production-quality cinematic
";

        return new StoryboardFrameResponse
        {
            ShotNumber = shot.ShotNumber,
            Prompt = prompt,
            NegativePrompt = "low quality, blurry, distorted faces, extra limbs",
            Style = "cinematic, ultra realistic, film still",
            Camera = shot.CameraDescription,
            Lighting = "cinematic lighting",
            Composition = "rule of thirds"
        };
    }
}
