<script setup>
import { computed } from 'vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { AppState } from '../AppState.js';
import { favoriteService } from '../services/FavoriteService.js';
import { recipeService } from '../services/RecipeService.js';

const account = computed(()=> AppState.account)
const recipe = computed(()=>AppState.activeRecipe)
const ingredients = computed(()=> AppState.ingredients)
// const favorite = computed(()=> AppState.favorites.find(()=> favorite.value.recipeId == favorite.value.accountId))
const owner = computed(()=> AppState.account.id == recipe.value.creatorId)
const favorited = computed(()=> AppState.favorites.find(favorite => favorite.recipeId == `${recipe.value.id}`))

async function favoriteRecipe(recipeId){
    try {
      await favoriteService.favorite(recipeId)
            Pop.success("Favorite!")
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
function edit(){
  try {
    recipeService.prepareEdit()
  }
  catch (error){
    Pop.toast("Unable to edit", 'error');
    logger.error("unable to edit", error);
  }
}

async function trashRecipe(recipeId){
  try {
    const confirm = await Pop.confirm("Do you want to delete this recipe and all its ingredients?")
    if(!confirm){
      return
    }
    await recipeService.trashRecipe(recipeId)
  }
  catch (error){
    Pop.toast("Unable to remove recipe", 'error');
    logger.error("failed to delete recipe", error);
  }
}
</script>


<template>
  <div class="rounded row">
    <div class="rounded-start-bottom imgCard col-md-4 col-12" :style="{backgroundImage: `url(${recipe?.img})`}">
      <div class="d-flex justify-content-between align-items-center px-1 mb-5">
        <span class="rounded bg-dark text-light opacity-75 p-1">{{ recipe?.category }}</span>
        <div v-if="account">
          <div v-if="favorited != null" class="rounded-bottom bg-dark text-light opacity-75 p-1"
            @click="removeFavorite(favorited?.id)">
            <i class="mdi mdi-heart fs-4 text-danger"></i></div>
          <div v-else-if="favorited == null" class="rounded-bottom bg-dark text-light opacity-75 p-1"
            @click="favoriteRecipe(recipe?.id)"><i class="mdi mdi-heart-outline fs-4"></i></div>
        </div>
      </div>
    </div>
    <div class="col-md-4 col-12 my-2">
      <h5>{{ recipe?.creator.name }}</h5>
      <div>
        <span>{{ recipe?.instructions }}</span>
      </div>
    </div>
    <div class="col-md-4 col-12 my-2">
      <div>
        <h5>Ingredients</h5>
        <p v-for="ingredient in ingredients" :key="ingredient?.id">{{ ingredient?.quantity }} |
          {{ ingredient?.name }}</p>
      </div>
      <div v-if="owner" class="mt-5 text-end">
        <span class="text-warning rounded-circle p-1 fs-1 text-end" role="button" @click="edit()"><i
            class="mdi mdi-pencil"></i></span>
        <span @click="trashRecipe(recipe.id)"><i class="mdi mdi-trash-can text-danger"></i></span>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.imgCard{
    background-position: center;
    background-size:cover;
    height: 100dvh;
}
</style>