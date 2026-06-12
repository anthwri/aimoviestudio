<template>
  <div>
    <h2>📦 Job Queue</h2>
    <p>Pending / Running / Failed / Complete</p>
    <div class='grid'>
      <div class='box'>Pending: {{ pending }}</div>
      <div class='box'>Running: {{ running }}</div>
      <div class='box'>Failed: {{ failed }}</div>
      <div class='box'>Complete: {{ complete }}</div>
    </div>
  </div>
</template>

<script setup>
defineProps({
  pending: Number,
  running: Number,
  failed: Number,
  complete: Number
})
</script>

<style>
.grid { display: flex; gap: 10px; }
.box {
  padding: 10px;
  background: #222;
  color: white;
}
</style>
