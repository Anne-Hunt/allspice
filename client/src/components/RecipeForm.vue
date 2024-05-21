<script setup>
import { computed, onUnmounted, ref } from 'vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { recipeService } from '../services/RecipeService.js';
import { AppState } from '../AppState.js';

const activeRecipe = computed(()=> AppState.activeRecipe)

const recipe = ref({
    title: '',
    img: '',
    category: '',
    instructions: ''
})

async function createRecipe(){
    try {
      const recipeData = recipe
      await recipeService.createRecipe(recipeData)

    }
    catch (error){
      Pop.toast("Unable to submit recipe", 'error');
      logger.error("Unable to submit recipe", error)
    }
}

async function updateRecipe(recipeId){
try {
  const recipeData = recipe
  await recipeService.updateRecipe(recipeData, recipeId)
  resetForm()
}
catch (error){
  Pop.toast("Unable to update recipe", 'error');
  logger.error("Unable to update recipe", error)
}
}

function resetForm(){
  recipe.value.title = '',
  recipe.value.instructions= ''
}

onUnmounted(()=>{
  resetForm()
})
</script>


<template>
<form v-if="activeRecipe == null" @submit.prevent="createRecipe()">
  <div class="mb-3">
    <label for="title" class="form-label">Recipe Title</label>
    <input v-modal="recipe.title" type="text" class="form-control" id="titleInput" placeholder="">
  </div>
  <div class="mb-3 row">
    <div class="col">
      <label for="image" class="form-label">Image</label>
      <input v-modal="recipe.img" type="text" class="form-control" id="imgInput" placeholder="">
    </div>
    <div class="col">
      <label for="category" class="form-label">Category</label>
      <select v-modal="recipe.category" class="form-select" id="categoryInput" aria-label="select-category">
        <option value="breakfast">Breakfast</option>
        <option value="lunch">Lunch</option>
        <option value="dinner">Dinner</option>
        <option value="snacks">Snacks</option>
        <option value="dessert">Dessert</option>
      </select>
    </div>
  </div>
  <div class="mb-3">
    <label for="instructions" class="form-label">Instructions</label>
    <textarea v-modal="recipe.instructions" class="form-control" id="instructionsInput" rows="5"></textarea>
  </div>
  <button class="btn btn-outline-secondary" type="submit" id="ingredientButton">Submit</button>
</form>

<form v-else @submit.prevent="updateRecipe()">
  <div class="mb-3">
    <label for="title" class="form-label">Recipe Title</label>
    <input v-modal="recipe.title" type="text" class="form-control" id="titleInput" :placeholder="activeRecipe.title">
  </div>
  <div class="mb-3 row">
    <div class="col">
      <label for="image" class="form-label">Image</label>
      <input v-modal="recipe.img" type="text" class="form-control" id="imgInput" :placeholder="activeRecipe.img">
    </div>
    <div class="col">
      <label for="category" class="form-label">Category</label>
      <select v-modal="recipe.category" class="form-select" id="categoryInput" aria-label="select-category">
        <option value="breakfast">Breakfast</option>
        <option value="lunch">Lunch</option>
        <option value="dinner">Dinner</option>
        <option value="snacks">Snacks</option>
        <option value="dessert">Dessert</option>
      </select>
    </div>
  </div>
  <div class="mb-3">
    <label for="instructions" class="form-label">Instructions</label>
    <textarea v-modal="recipe.instructions" class="form-control" id="instructionsInput" rows="5" :placeholder="activeRecipe.instructions"></textarea>
    <button class="btn btn-outline-secondary" type="submit" id="ingredientButton">Update</button>
  </div>
</form>
</template>


<style lang="scss" scoped>

</style>