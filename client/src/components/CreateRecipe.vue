<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';

const newRecipe = computed(()=> AppState.newRecipe)
</script>


<template>
    <div v-if="newRecipe" class="col-md-4 rounded-start">
        <img class="img-fluid rounded-start" :src="newRecipe?.img">
    </div>
    <div class="col-md-4 col-10 my-2">
        <RecipeForm />
    </div>
    <div v-if="newRecipe" class="col-md-4 col-10 my-2">
        <IngredientForm />
    </div>
</template>


<style lang="scss" scoped>

</style>