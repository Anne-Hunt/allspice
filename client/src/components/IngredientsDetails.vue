<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';

const ingredients = computed(()=>AppState.ingredients)
</script>


<template>
<div class="card">
    <div class="card-title bg-success rounded-top text-light text-center">
        <strong>Ingredients</strong>
    </div>
    <div class="card-body">
        <span v-for="ingredient in ingredients" :key="ingredient.id">{{ ingredient.quantity }} | {{ ingredient.name }}</span>
    </div>
</div>
</template>


<style lang="scss" scoped>

</style>