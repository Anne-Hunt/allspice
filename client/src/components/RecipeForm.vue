<script setup>
import { computed, onUnmounted, ref } from 'vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { recipeService } from '../services/RecipeService.js';
import { AppState } from '../AppState.js';

const newRecipe = computed(()=> AppState.newRecipe)
// const foundRecipe = computed(()=> {if(newRecipe.value != null) recipeInput.value = newRecipe.value; return true})
const user = computed(()=>AppState.account)
const recipeData = {}
const recipeInput = ref({
    title: '',
    img: '',
    category: '',
    instructions: ''
})

// const editRecipe = ({
//   title: newRecipe?.value.title || '',
//   img: newRecipe?.value.img || '',
//   category: newRecipe?.value.category || '',
//   instructions: newRecipe?.value.instructions || ''
// })

async function createRecipe(){
    try {
      recipeData.title = recipeInput.value.title
      recipeData.img = recipeInput.value.img
      recipeData.category = recipeInput.value.category
      recipeData.instructions = recipeInput.value.instructions
      recipeData.creatorId = user.value.id
      await recipeService.createRecipe(recipeData)
    }
    catch (error){
      Pop.toast("Unable to submit recipe", 'error');
      logger.error("Unable to submit recipe", error)
    }
}

async function updateRecipe(recipeId){
try {
  // const recipeData = editRecipe
  const recipeData = recipeInput
  await recipeService.updateRecipe(recipeData, recipeId)
}
catch (error){
  Pop.toast("Unable to update recipe", 'error');
  logger.error("Unable to update recipe", error)
}
}

function resetForm(){
  recipeInput.value.title = ''
  recipeInput.value.instructions= ''
  recipeInput.value.category = ''
  recipeInput.value.img = ''
}

onUnmounted(()=>{
  resetForm()
})
</script>


<template>
  <div>
      <form v-if="newRecipe == null" @submit.prevent="createRecipe()">
        <div class="mb-3">
          <label for="title" class="form-label">Recipe Title</label>
          <input v-model="recipeInput.title" type="text" class="form-control" id="titleInput" placeholder="">
        </div>
        <div class="mb-3 row">
          <div class="col">
            <label for="image" class="form-label">Image</label>
            <input v-model="recipeInput.img" type="text" class="form-control" id="imgInput" placeholder="">
          </div>
          <div class="col">
            <label for="category" class="form-label">Category</label>
            <select v-model="recipeInput.category" class="form-select" id="categoryInput" aria-label="select-category">
              <option value="breakfast">Breakfast</option>
              <option value="lunch">Lunch</option>
              <option value="dinner">Dinner</option>
              <option value="snack">Snacks</option>
              <option value="dessert">Dessert</option>
            </select>
          </div>
        </div>
        <div class="mb-3">
          <label for="instructions" class="form-label">Instructions</label>
          <textarea v-model="recipeInput.instructions" class="form-control" id="instructionsInput" rows="5"></textarea>
        </div>
        <button class="btn btn-outline-dark" type="submit" id="createButton">Submit</button>
      </form>
      <form v-else @submit.prevent="updateRecipe(newRecipe?.id)">
        <div class="mb-3">
          <label for="title" class="form-label">Recipe Title</label>
          <input v-model="recipeInput.title" type="text" class="form-control" id="titleInput">
        </div>
        <div class="mb-3 row">
          <div class="col">
            <label for="image" class="form-label">Image</label>
            <input v-model="recipeInput.img" type="text" class="form-control" id="imgInput">
          </div>
          <div class="col">
            <label for="category" class="form-label">Category</label>
            <select v-model="recipeInput.category" class="form-select" id="categoryInput" aria-label="select-category">
              <option value="breakfast">Breakfast</option>
              <option value="lunch">Lunch</option>
              <option value="dinner">Dinner</option>
              <option value="snacks">Snack</option>
              <option value="dessert">Dessert</option>
            </select>
          </div>
        </div>
        <div class="mb-3">
          <label for="instructions" class="form-label">Instructions</label>
          <textarea v-model="recipeInput.instructions" class="form-control" id="instructionsInput" rows="5"></textarea>
        </div>
        <button class="btn btn-outline-dark" type="submit" id="updateButton">Update</button>
      </form>
    </div>
</template>


<style lang="scss" scoped>

</style>