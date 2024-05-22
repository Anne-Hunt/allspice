<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import Navbar from '../components/Navbar.vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { accountService } from '../services/AccountService.js';

const account = computed(() => AppState.account)
const recipes = computed(()=> AppState.recipes.filter(recipe => recipe.creatorId == AppState.account.id))
const favorites = computed(()=> AppState.recipeFans.filter(recipe => recipe.favorite.creatorId == AppState.account.id))

async function editAccount(){
  try {
    const accountUpdated = await accountService.updateAccount()
    logger.log(accountUpdated)
  }
  catch (error){
    Pop.toast("Unable to update account", 'error');
    logger.error("unable to update account", error);
  }
}
</script>

<template>
<div class="container-fluid m-0 p-0">
  <div class="container-fluid heroImg shadow m-0 p-0">
    <Navbar class="bg-dark"/>
    </div>
  <div class="about text-center">
    <div v-if="account">
      <div class="text-end">
        <button class="btn btn-outline-success" @click="editAccount()">Edit Account</button>
      </div>
      <h1>Welcome {{ account.name }}</h1>
      <img class="rounded" :src="account.picture" alt="" />
      <p>{{ account.email }}</p>
    </div>
    <div v-else>
      <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
    </div>
  </div>
  <div class="row g-3 mt-5 px-3">
    <h4>Your Recipes</h4>
    <div class="col-md-4 col-6 d-flex" v-for="recipe in recipes" :key="recipe?.id">
      <RecipeCard :recipe="recipe"/>
    </div>
    <h4>Your Favorites</h4>
    <div class="col-md-4 col-6 d-flex" v-for="favorite in favorites" :key="favorite?.id">
      <RecipeCard :recipe="favorite"/>
    </div>
    </div>
    </div>
</template>

<style scoped lang="scss">
img {
  max-width: 100px;
}

.heroImg{
  background-image: url("https://plus.unsplash.com/premium_photo-1681401570418-4054ba349fa3?q=80&w=2670&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D");
  background-size: cover;
  height: 50dvh;
}
</style>
