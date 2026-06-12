<template>
  <div class='dashboard'>
    <h1>🎬 AI Movie Studio Control Room</h1>

    <div class='card'>
      <h2>Studio State</h2>
      <pre>{{ state }}</pre>
    </div>

    <button @click='refresh'>Refresh</button>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { getStudioState } from '../api/studioApi'

const state = ref({})

async function refresh() {
  state.value = await getStudioState()
}

onMounted(refresh)
</script>

<style>
.dashboard {
  font-family: Arial;
  padding: 20px;
}
.card {
  background: #111;
  color: #0f0;
  padding: 10px;
  margin: 10px 0;
}
</style>
