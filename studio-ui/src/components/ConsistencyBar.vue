<template>
  <div>
    <h2>🎯 Consistency Score</h2>

    <div class='bar'>
      <div class='fill' :style='{width: score + \"%\"}'></div>
    </div>

    <p>{{ score }}%</p>
  </div>
</template>

<script setup>
defineProps({
  score: Number
})
</script>

<style>
.bar {
  width: 100%;
  background: #333;
  height: 20px;
}
.fill {
  height: 20px;
  background: limegreen;
}
</style>
