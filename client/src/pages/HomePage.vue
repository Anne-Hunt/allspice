<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import { recipeService } from '../services/RecipeService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';


const recipes = computed(()=>AppState.recipes)

async function getRecipes(){
  try {
    await recipeService.getRecipes()
  } catch (error) {
    Pop.toast("Unable to get recipes", 'error')
    logger.error("Unable to get recipes", error)
  }
}

onMounted(()=>
{getRecipes()})
</script>

<template>
<div class="container-fluid">
  <div class="row">
    <div class="col-12 heroImg shadow"></div>
    <div class="row justify-content-center">
      <div class="col-6 shadow">
        <div class="row p-3 text-success">
          <div class="col text-center">Home</div>
          <div class="col text-center">My Recipes</div>
          <div class="col text-center">Favorites</div>
        </div>
      </div>
    </div>
  </div>
  <div class="row g-3 mt-3">
    <div class="col-md-3 col-2 d-flex" v-for="recipe in recipes" :key="recipe?.id">
      <RecipeCard :recipe="recipe"/>
    </div>
  </div>
</div>
</template>

<style scoped lang="scss">
.heroImg{
  background-image: url("https://images.unsplash.com/photo-1455619452474-d2be8b1e70cd?q=80&w=2370&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D");
  background-size: cover;
  height: 50dvh;
}
</style>
