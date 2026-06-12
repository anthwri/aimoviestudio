using Domain.Franchise;

namespace Application.Franchise;

public sealed class CharacterEvolutionEngine
{
    private readonly Dictionary<Guid, CharacterLifecycle> _characters = new();

    public CharacterLifecycle GetOrCreate(Guid id, string name)
    {
        if (!_characters.ContainsKey(id))
        {
            _characters[id] = new CharacterLifecycle
            {
                CharacterId = id,
                Name = name,
                Age = 25
            };
        }

        return _characters[id];
    }

    public void ApplyEvent(Guid id, string eventDescription)
    {
        var character = _characters[id];

        character.MajorEvents.Add(eventDescription);

        if (eventDescription.Contains("years later"))
        {
            character.Age += 5;
        }

        if (eventDescription.Contains("trauma"))
        {
            character.CurrentArcState = "damaged";
        }
    }
}
