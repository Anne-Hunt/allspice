<script setup>
import { Recipe } from '../models/Recipe.js';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { favoriteService } from '../services/FavoriteService.js';
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import { Favorite } from '../models/Favorite.js';
import { RecipeFan } from '../models/RecipeFan.js';
import { recipeService } from '../services/RecipeService.js';
import { Modal } from 'bootstrap';


const props = defineProps({recipe: Recipe, favorite: Favorite, recipeFan: RecipeFan})

const account = computed(()=>AppState.account)
const favorited = computed(()=> AppState.favorites.find(favorite => favorite.recipeId == `${props.recipe.id}`))

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

async function removeFavorite(favoriteId){
    try {
      await favoriteService.trashFavorite(favoriteId)
    }
    catch (error){
      Pop.toast("Cannot remove favorite at this time", 'error');
      logger.error("Unable to remove favorite", error)
    }
}

async function setActiveRecipe(recipeId){
    await recipeService.setActiveRecipe(recipeId)
    Modal.getOrCreateInstance('#recipeModal')
}

</script>


<template>
    
    <div class="card rounded imgCard shadow m-0 p-0 w-100" :style="{backgroundImage: `url(${recipeFan?.img})`}" type="button"   data-bs-toggle="modal" data-bs-target="#recipeModal" @click="setActiveRecipe(recipeFan.id)">
            <div class="d-flex justify-content-between align-items-center p-0 mb-5">
                <span class="rounded bg-dark text-light opacity-75 m-0 p-2">{{ recipeFan?.category }}</span>
                <div v-if="account">
                    <div v-if="favorited != null" class="rounded-bottom bg-dark text-light opacity-75 p-1" @click="removeFavorite(favorited?.id)">
                        <i class="mdi mdi-heart fs-4 text-danger"></i></div>
                        <div v-else-if="favorited == null" class="rounded-bottom bg-dark text-light opacity-75 p-1" @click="favoriteRecipe(recipeFan?.id)"><i class="mdi mdi-heart-outline fs-4"></i></div>
                    </div>
                </div>
                <div class="card-body text-truncated d-flex align-content-end flex-wrap m-0 p-0">
                    <div class="bg-dark rounded-bottom text-light opacity-75 w-100 p-1">
                        <span class="card-text"><strong>{{ recipeFan?.title }}</strong></span>
                        <p class="card-text text-truncate">{{ recipeFan?.instructions }}</p>
                    </div>
                </div>
            </div>
</template>


<style lang="scss" scoped>
.imgCard{
    background-position: center;
    background-size:cover;
    height: 30dvh;
}
</style>