using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs;

public sealed class FilmHub : Hub
{
    public async Task JoinFilmRoom(string filmId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, filmId);
    }

    public async Task LeaveFilmRoom(string filmId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, filmId);
    }
}
