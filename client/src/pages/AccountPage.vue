<script setup>
import { computed, onMounted, ref } from 'vue';
import { AppState } from '../AppState.js';
import Navbar from '../components/Navbar.vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { accountService } from '../services/AccountService.js';

const account = computed(() => AppState.account)
const recipes = computed(()=> AppState.recipes.filter(recipe => recipe.creatorId == AppState.account.id))
const favorites = computed(()=> AppState.recipeFans.filter(recipe => recipe.favorite.creatorId == AppState.account.id))
const accountData = ref({
	name: '',
	picture: '',
})
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

onMounted(()=>
accountData.value = { ...AppState.account } )
</script>

<template>
<div class="container-fluid m-0 p-0">
  <div class="container-fluid shadow m-0 p-0">
    <Navbar class="bg-dark"/>
    <div class="row p-0 m-0 heroImg justify-content-center align-content-bottom text-center" :style="{backgroundImage: `url(${account?.coverImg})`}">
      <h1 class="text-light pb-5 fontfix">Welcome {{ account.name }}</h1>
      <div class="pt-5">

        <img class="rounded-circle border border-light shadow border-3 profileImg p-0" :src="account.picture" alt="" />
      </div>
      <div v-if="account">
        <div class="text-end">
          <i class="mdi mdi-dots-horizontal fontfix text-light fs-1" type="button" data-bs-toggle="offcanvas" data-bs-target="#offCanvas" aria-controls="offCanvas"></i>
        </div>
      </div>
    </div>
    </div>
  <div class="about text-center">
  </div>
  <div v-if="recipes.length > 0" class="row g-3 mt-5 px-3">
    <h4>Your Recipes</h4>
    <div class="col-md-4 col-6 d-flex" v-for="recipe in recipes" :key="recipe?.id">
      <RecipeCard :recipe="recipe"/>
    </div>
  </div>
  <div v-else class="row g-3 mt-5 px-3"><h4>Click the <i class="mdi mdi-plus-circle text-success"></i> below to add a recipe!</h4></div>
  <div v-if="favorites.length > 0" class="row g-3 mt-5 px-3">

    <h4>Your Favorites</h4>
    <div class="col-md-4 col-6 d-flex" v-for="favorite in favorites" :key="favorite?.id">
      <RecipeCard :recipe="favorite"/>
    </div>
  </div>
  <div v-else class="row g-3 mt-5 px-3"><h4>Return to the home page and <i class="mdi mdi-heart text-danger"></i> some recipes to add favorites!</h4></div>
</div>


<Offcanvas></Offcanvas>
</template>

<style scoped lang="scss">

.profileImg{
  height: 100px;
  width: 100px;
  object-fit: cover;
  object-position: center;
  // position:absolute;
  // bottom: 50px;
}

.heroImg{
  background-size: cover;
  height: 35dvh;
}

.fontfix{
  text-shadow: 1px 1px 4px black;
}
</style>
