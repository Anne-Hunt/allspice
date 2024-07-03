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
  <div class="container-fluid heroImg shadow m-0 p-0">
    <Navbar class="bg-dark"/>
    </div>
  <div class="about text-center">
    <div v-if="account">
      <div class="text-end">
        <button class="btn btn-primary" type="button" data-bs-toggle="offcanvas" data-bs-target="#accountEditForm" aria-controls="accountEditForm">
  Edit Account
</button>

      </div>
      <h1>Welcome {{ account.name }}</h1>
      <img class="rounded-circle border border-danger border-2" :src="account.picture" alt="" />
      <p>{{ account.name }}</p>
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

    <!--OFFCANVAS-->
    <div class="offcanvas offcanvas-start" tabindex="-1" id="editAccountForm" aria-labelledby="accountFormLabel">
  <div class="offcanvas-header">
    <h5 class="offcanvas-title" id="accountFormLabel">Edit Your Account</h5>
    <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
  </div>
  <div class="offcanvas-body">
    <div>
      <form @submit.prevent="editAccount()">
        <div class="mb-3">
  <label for="nameInput" class="form-label">Name</label>
  <input type="text" class="form-control" id="nameInput" v-model="accountData.name">
</div>
<div class="mb-3">
  <label for="imageUrl" class="form-label">Picture</label>
  <input type="text" class="form-control" id="imageUrl" v-model="accountData.picture">
</div>
<button type="submit" class="btn btn-primary">Submit</button>
      </form>
    </div>
  </div>
</div>
</template>

<style scoped lang="scss">
img {
  width: 100px;
  height: 100px;
  object-fit: cover;
  object-position: center;
}

.heroImg{
  background-image: url("https://plus.unsplash.com/premium_photo-1681401570418-4054ba349fa3?q=80&w=2670&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D");
  background-size: cover;
  height: 50dvh;
}
</style>
