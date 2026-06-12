import axios from 'axios';

export class FilmApi {
    static async createFilm(idea) {
        return await axios.post('http://localhost:5000/api/film/execute', {
            idea,
            maxScenes: 5,
            generateImages: true
        });
    }
}
