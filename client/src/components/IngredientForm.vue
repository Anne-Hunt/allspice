<script setup>
import { computed, ref } from 'vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { AppState } from '../AppState.js';
import { ingredientService } from '../services/IngredientService.js';

const ingredients = computed(()=> AppState.ingredients)
const ingredientData = {}
const ingredient = ref({
  quantity: '',
  name: ''
})

async function createIngredient(){
    try {
      const recipeId = AppState.activeRecipe.id
      ingredientData.recipeId = recipeId
      ingredientData.quantity = ingredient.value.quantity
      ingredientData.name = ingredient.value.name
      await ingredientService.createIngredients(ingredientData)
      resetForm()
    }
    catch (error){
      Pop.toast("Unable to submit ingredient", 'error');
      logger.error("Unable to submit ingredient", error)
    }
}

async function updateIngredient(ingredientId){
  try {
    const recipeId = AppState.activeRecipe.id
      ingredientData.recipeId = recipeId
      ingredientData.quantity = ingredient.value.quantity
      ingredientData.name = ingredient.value.name
    await ingredientService.updateIngredient(ingredientData, ingredientId)
    resetForm()
  }
  catch (error){
    Pop.toast("Unable to update",'error');
    logger.error("Unable to update ingredient", error)
  }
}

async function trashIngredient(ingredientId){
  try {
    Pop.confirm("Do you really want to delete this ingredient?")
    if(!confirm){
      return
    }
    ingredientService.trashIngredient(ingredientId)
    resetForm()
  }
  catch (error){
    Pop.toast("Unable to delete", 'error');
    logger.error("Unable to delete ingredient", error)
  }
}

function resetForm(){
    ingredient.value.quantity = ''
    ingredient.value.name = ''
}
</script>


<template>
  <div class="card">
    <div class="card-header bg-success">
      <h2>Ingredients</h2>
    </div>
    <div class="card-body">

      <div v-if="ingredients">
        <form v-for="ingredient in ingredients" :key="ingredient.id" @submit.prevent="updateIngredient(ingredient.id)">
          <div class="input-group mb-3">
            <input v-model="ingredient.quantity" type="text" class="form-control" :placeholder="ingredient.quantity" aria-label="Quantity">
            <span class="input-group-text">OF</span>
            <input v-model="ingredient.name" type="text" class="form-control" :placeholder="ingredient.name" aria-label="Name" maxlength="250" aria-describedby="ingredientButton" name="ingredient" id="ingredient">
            <button class="btn btn-outline-primary" type="submit" id="ingredientButton"><i class="mdi mdi-pencil"></i></button>
            <button class="btn btn-outline-danger" type="button" id="ingredientButton" @click="trashIngredient(ingredient.id)"><i class="mdi mdi-trash-can"></i></button>
          </div>
        </form>
      </div>
      <div>
        <form @submit.prevent="createIngredient()">  
          <div class="input-group mb-3">
            <input v-model="ingredient.quantity" type="text" class="form-control" placeholder="Quantity" aria-label="Quantity">
            <span class="input-group-text">OF</span>
            <input v-model="ingredient.name" type="text" class="form-control" placeholder="Name" aria-label="Name" maxlength="250" aria-describedby="ingredientButton" name="ingredient" id="ingredient">
            <button class="btn btn-outline-success" type="submit" id="ingredientButton"><i class="mdi mdi-plus"></i></button>
          </div>
        </form>
      </div>
    </div>
    </div>
</template>


<style lang="scss" scoped>

</style>