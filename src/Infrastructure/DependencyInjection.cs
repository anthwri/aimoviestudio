using Application.Interfaces.Llm;
using Application.Interfaces.Persistence;
using Infrastructure.Database;
using Infrastructure.Ollama;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<MovieDbContext>(opt =>
            opt.UseNpgsql(
                config.GetConnectionString("DefaultConnection")));

        services.AddHttpClient<ILlmClient, OllamaClient>(client =>
        {
            client.BaseAddress = new Uri(
                config["Ollama:BaseUrl"]!);
        });

        services.Configure<OllamaOptions>(
            config.GetSection("Ollama"));

        services.AddScoped<IMovieRepository, MovieRepository>();

        return services;
    }
}
