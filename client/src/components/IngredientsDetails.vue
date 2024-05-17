<script setup>
import { Ingredient } from '../models/Ingredient.js';


defineProps({ingredient: Ingredient})
</script>


<template>
<div class="card bg-success rounded opacity-25 shadow">
    <div class="card-title bg-success rounded-top text-light text-center">
        <strong>Ingredients</strong>
    </div>
    <div class="card-body">
        <span v-for="ingredient in ingredients" :key="ingredient">{{ ingredient.quantity }} | {{ ingredient.name }}</span>
    </div>
</div>
</template>


<style lang="scss" scoped>

</style>