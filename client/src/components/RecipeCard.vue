<script setup>
import { computed } from 'vue';
import { Recipe } from '../models/Recipe.js';
import { AppState } from '../AppState.js';


defineProps({recipe: Recipe})
const favorite = computed(()=> AppState.favorites.find(()=> favorite.value.recipeId == favorite.value.accountId))
// const recipeImg = computed(()=>`url('${recipe.img}')`)

function openModal(){
    
}
</script>


<template>
        <div class="card rounded imgCard shadow m-0 p-0" :style="{backgroundImage: `url(${recipe.img})`}" @click="openModal()">
            <div class="d-flex justify-content-between align-items-center px-1 mb-5">
                <span class="rounded bg-dark text-light opacity-75 p-1">{{ recipe.category }}</span>
                <div class="rounded-bottom bg-dark text-light opacity-75 p-1"><i v-if="favorite"
                        class="mdi mdi-heart fs-4"></i><i v-else class="mdi mdi-heart-outline fs-4"></i></div>
            </div>
            <div class="card-body text-truncated d-flex align-content-end flex-wrap m-0 p-0">
                <div class="bg-dark rounded-bottom text-light opacity-75 w-100 p-1">
                    <span class="card-text"><strong>{{ recipe.title }}</strong></span>
                    <p class="card-text text-truncate">{{ recipe.instructions }}</p>
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