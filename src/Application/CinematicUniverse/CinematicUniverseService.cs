using Domain.CinematicUniverse;

namespace Application.CinematicUniverse;

public sealed class CinematicUniverseService
{
    private readonly CinematicUniverseMemory _memory = new();
    private readonly IdentityPromptLocker _locker = new();
    private readonly VisualDriftDetector _drift = new();
    private readonly AutoRegenerationController _regen = new();

    private readonly Dictionary<Guid, CharacterVisualAnchor> _characters = new();

    public CharacterVisualAnchor RegisterCharacter(string name, string basePrompt)
    {
        var anchor = new CharacterVisualAnchor
        {
            Name = name,
            BasePrompt = basePrompt,
            StyleLock = ""cinematic realism, ultra consistent character identity""
        };

        _characters[anchor.CharacterId] = anchor;

        _memory.StoreCharacterEvent(name, ""Character created"");

        return anchor;
    }

    public RenderJob PrepareRender(RenderJob job, Guid characterId, string outputHint)
    {
        var anchor = _characters[characterId];

        job.Prompt = _locker.Apply(anchor, job.Prompt);

        var drift = _drift.Detect(anchor.BasePrompt, outputHint);

        job = _regen.FixIfNeeded(job, drift);

        if (drift)
        {
            _memory.StoreCharacterEvent(anchor.Name, ""Drift detected and corrected"");
        }

        return job;
    }
}
