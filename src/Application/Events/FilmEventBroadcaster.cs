using Api.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Application.Events;

public sealed class FilmEventBroadcaster
{
    private readonly IHubContext<FilmHub> _hub;

    public FilmEventBroadcaster(IHubContext<FilmHub> hub)
    {
        _hub = hub;
    }

    public async Task PublishAsync(FilmProgressEvent evt)
    {
        await _hub.Clients
            .Group(evt.FilmId.ToString())
            .SendAsync(""film-update"", evt);
    }
}
