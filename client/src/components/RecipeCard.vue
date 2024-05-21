<script setup>
import { Recipe } from '../models/Recipe.js';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { favoriteService } from '../services/FavoriteService.js';
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import { Favorite } from '../models/Favorite.js';
import { RecipeFan } from '../models/RecipeFan.js';
import ModalRecipe from './ModalRecipe.vue';


const props = defineProps({recipe: Recipe, favorite: Favorite, recipeFan: RecipeFan})

const account = computed(()=>AppState.account)
const favorited = computed(()=> AppState.favorites.find(favorite => favorite.recipeId == `${props.recipe.id}`))

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

// function findFavorite(){
//     const favorited = AppState.favorites.filter(favorite => favorite.recipeId == props.recipe.id)
//     const userFavorite = favorited.find(favorite => favorite.creatorId == account.value.id)
//     fave.value.id = userFavorite.id
// }

</script>


<template>
    
    <div class="card rounded imgCard shadow m-0 p-0" :style="{backgroundImage: `url(${recipe?.img})`}" role="button" data-bs-toggle="modal" :data-bs-target="`#modal-${recipe?.id}`">
            <div class="d-flex justify-content-between align-items-center px-1 mb-5">
                <span class="rounded bg-dark text-light opacity-75 p-1">{{ recipe?.category }}</span>
                <div v-if="account">
                    <div v-if="favorited != null" class="rounded-bottom bg-dark text-light opacity-75 p-1" @click="removeFavorite(recipe?.id)">
                        <i class="mdi mdi-heart fs-4 text-danger"></i></div>
                        <div v-else-if="favorited == null" class="rounded-bottom bg-dark text-light opacity-75 p-1" @click="favoriteRecipe(recipe?.id)"><i class="mdi mdi-heart-outline fs-4"></i></div>
                    </div>
                </div>
                <div class="card-body text-truncated d-flex align-content-end flex-wrap m-0 p-0">
                    <div class="bg-dark rounded-bottom text-light opacity-75 w-100 p-1">
                        <span class="card-text"><strong>{{ recipe?.title }} {{ recipe?.id }}</strong></span>
                        <p class="card-text text-truncate">{{ recipe?.instructions }}</p>
                    </div>
                </div>
            </div>
        <ModalRecipe :id="`#modal-${recipe?.id}`"/>
</template>


<style lang="scss" scoped>
.imgCard{
    background-position: center;
    background-size:cover;
    height: 30dvh;
}
</style>