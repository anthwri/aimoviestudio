import * as signalR from '@microsoft/signalr';

export class FilmHubClient {
    constructor() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl('http://localhost:5000/hub/film')
            .withAutomaticReconnect()
            .build();
    }

    async start(onUpdate) {
        this.connection.on('film-update', onUpdate);
        await this.connection.start();
    }

    joinFilm(filmId) {
        this.connection.invoke('JoinFilmRoom', filmId);
    }
}
