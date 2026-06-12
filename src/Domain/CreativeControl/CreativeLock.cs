namespace Domain.CreativeControl;

public sealed class CreativeLock
{
    public bool LockCharacters { get; set; }

    public bool LockLocations { get; set; }

    public bool LockStoryEnding { get; set; }

    public bool LockVisualStyle { get; set; }
}
