<script setup>
import { computed, onMounted, ref } from 'vue';
import { AppState } from '../AppState.js';
import { recipeService } from '../services/RecipeService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { accountService } from '../services/AccountService.js';
import Navbar from '../components/Navbar.vue';

const recipes = computed(()=>{
  if(filterBy.value == 'all') {return AppState.recipes}
  if(filterBy.value == 'favorites') {return AppState.favorites}
  if(filterBy.value == 'created'){return AppState.userRecipes}
    else
  return AppState.recipes.filter(recipe => recipe.category == filterBy.value)})

const filterBy = ref('all')

async function getRecipes(){
  try {
    await recipeService.getRecipes()
  } catch (error) {
    Pop.toast("Unable to get recipes", 'error')
    logger.error("Unable to get recipes", error)
  }
}

async function getFavorites(){
  try {
    if(!AppState.account){
      return
    }
    await accountService.getFavorites()
    logger.log(AppState.favorites)
  }
  catch (error){
    Pop.toast("Unable to get favorites", 'error');
    logger.error("Unable to get favorites", error)
  }
}

async function getUserRecipes(){
  try {
    if(!AppState.account){
      return
    }
    await accountService.getRecipes()
    logger.log(AppState.userRecipes)
  }
  catch (error){
    Pop.toast("Unable to get user recipes", 'error');
    logger.error("Unable to get user recipes", error)
  }
}

onMounted(()=>
  {getRecipes()
    getFavorites()
    getUserRecipes()
})
</script>

<template>
<div class="container-fluid m-0 p-0">
  <div class="container-fluid heroImg shadow">
    <Navbar class="fontfix"/>
    </div>
  <div class="row justify-content-center mb-4">
      <div class="col-6 shadow overlay">
        <div class="row p-3 bg-light text-success align-items-end">
          <div class="col text-center" type="button" @click="filterBy = 'all'"><strong>Home</strong></div>
          <div class="col text-center" type="button" @click="filterBy = 'created'"><strong>My Recipes</strong></div>
          <div class="col text-center" type="button" @click="filterBy = 'favorites'"><strong>Favorites</strong></div>
        </div>
      </div>
    </div>
  </div>
  <div class="row g-3 mt-5 px-3">
    <div class="col-md-4 col-6 d-flex" v-for="recipe in recipes" :key="recipe?.id">
      <RecipeCard :recipe="recipe"/>
    </div>
  </div>

</template>

<style scoped lang="scss">
.heroImg{
  background-image: url("https://images.unsplash.com/photo-1455619452474-d2be8b1e70cd?q=80&w=2370&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D");
  background-size: cover;
  height: 50dvh;
}

.overlay{
  position: absolute;
  z-index: 1;
  top: 45dvh;
}

.fontfix{
  text-shadow: 1px 1px 4px black;
}
</style>
