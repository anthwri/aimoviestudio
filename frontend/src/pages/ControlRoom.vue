<template>
  <div class='dashboard'>
    <h1>🎬 AI Film Control Room</h1>

    <div class='controls'>
      <input v-model='idea' placeholder='Enter film idea...' />
      <button @click='createFilm'>Generate Film</button>
    </div>

    <div class='status'>
      <h2>Live Production Feed</h2>

      <div v-for='event in events' :key='event.timestamp' class='event'>
        <span>[{{ event.status }}]</span>
        <span>{{ event.message }}</span>
      </div>
    </div>
  </div>
</template>

<script>
import { FilmApi } from '../services/filmApi';
import { FilmHubClient } from '../services/filmHub';

export default {
  data() {
    return {
      idea: '',
      events: [],
      hub: null
    }
  },

  async mounted() {
    this.hub = new FilmHubClient();

    await this.hub.start((event) => {
      this.events.unshift(event);
    });
  },

  methods: {
    async createFilm() {
      const res = await FilmApi.createFilm(this.idea);
      this.hub.joinFilm(res.data.movieId);
    }
  }
}
</script>

<style>
.dashboard {
  padding: 20px;
  font-family: Arial;
}

.controls {
  margin-bottom: 20px;
}

.event {
  padding: 5px;
  border-bottom: 1px solid #ddd;
}
</style>
