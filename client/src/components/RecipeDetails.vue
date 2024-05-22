<script setup>
import { computed } from 'vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { AppState } from '../AppState.js';
import { favoriteService } from '../services/FavoriteService.js';

const account = computed(()=> AppState.account)
const recipe = computed(()=>AppState.activeRecipe)
const ingredients = computed(()=> AppState.ingredients)
const favorite = computed(()=> AppState.favorites.find(()=> favorite.value.recipeId == favorite.value.accountId))
const owner = computed(()=> AppState.account.id == recipe.value.creatorId)
const favorited = computed(()=> AppState.favorites.find(favorite => favorite.recipeId == `${recipe.value.id}`))

async function favoriteRecipe(recipeId){
    try {
      await favoriteService.favorite(recipeId)
    }
    catch (error){
      Pop.toast("Cannot favorite at this time", 'error');
      logger.error("Unable to add favorite", error)
    }
}

async function removeFavorite(recipeId){
    try {
      await favoriteService.trashFavorite(recipeId)
    }
    catch (error){
      Pop.toast("Cannot remove favorite at this time", 'error');
      logger.error("Unable to remove favorite", error)
    }
}
</script>


<template>
  <div class="card rounded">
    <div class="card-image-start rounded-start">
      <div v-if="account">
        <div v-if="favorited != null" class="rounded-bottom bg-dark text-light opacity-75 p-1"
          @click="removeFavorite(recipe?.id)">
          <i class="mdi mdi-heart fs-4 text-danger"></i></div>
        <div v-else-if="favorited == null" class="rounded-bottom bg-dark text-light opacity-75 p-1"
          @click="favoriteRecipe(recipe?.id)"><i class="mdi mdi-heart-outline fs-4"></i></div>
      </div>
      <img :src="recipe?.img" :alt="recipe?.title">
    </div>
    <div class="body row">
      <span class="card-title">{{ recipe?.title }}</span>
      <span v-if="owner" class="bg-dark opacity-75 text-light rounded p-1"><i class="mdi mdi-pencil"></i></span>
      <div class="col-md-6 col-12 rounded">
        <span>{{ recipe?.instructions }}</span>
      </div>
      <div class="col-md-6 col-12 rounded">
        <span v-for="ingredient in ingredients" :key="ingredient?.id">{{ ingredient?.quantity }} |
          {{ ingredient?.name }}</span>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>

</style>