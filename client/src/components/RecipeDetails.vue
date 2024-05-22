<script setup>
import { computed, onMounted } from 'vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { recipeService } from '../services/RecipeService.js';
import { AppState } from '../AppState.js';
import { favoriteService } from '../services/FavoriteService.js';


const recipe = computed(()=>AppState.activeRecipe)
const ingredients = computed(()=> AppState.ingredients)
const favorite = computed(()=> AppState.favorites.find(()=> favorite.value.recipeId == favorite.value.accountId))
// const owner = computed(()=> AppState.account.id == AppState.activeRecipe?.creatorId)

async function favoriteRecipe(recipeId){
    try {
      const recipe = recipeId;
      await favoriteService.favorite(recipe)
    }
    catch (error){
      Pop.toast("Cannot favorite at this time", 'error');
      logger.error("Unable to alter favorite", error)
    }
}

async function getIngredients(recipeId){
    try {
      await recipeService.getIngredientsByRecipe(recipeId)
    }
    catch (error){
      Pop.toast("Unable to get ingredients",'error');
      logger.error("Unable to get ingredients for recipe", error)
    }
}

// onMounted(()=>
// getIngredients()
// )
</script>


<template>
    <div class="card rounded">
        <div class="card-image-start rounded-start">
            <div class="rounded-bottom bg-dark text-light opacity-75 p-1" @click="favoriteRecipe(recipe?.id)"><i v-if="favorite"
                        class="mdi mdi-heart fs-4"></i><i v-else class="mdi mdi-heart-outline fs-4"></i></div>
            <img :src="recipe?.img" :alt="recipe?.title">
        </div>
        <div class="body row">
            <span class="card-title">{{ recipe?.title }}</span>
            <!-- <span v-if="owner" class="bg-dark opacity-75 text-light rounded p-1"><i class="mdi mdi-pencil"></i></span> -->
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