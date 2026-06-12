namespace Infrastructure.Media;

public interface IImageGenerator
{
    Task<string> GenerateAsync(string prompt);
}

public interface IVideoGenerator
{
    Task<string> GenerateAsync(string storyboard);
}

public interface IAudioGenerator
{
    Task<string> GenerateAsync(string script);
}
