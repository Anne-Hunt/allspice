<script setup>
import { computed, ref } from 'vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { AppState } from '../AppState.js';
import { ingredientService } from '../services/IngredientService.js';

const ingredients = computed(()=> AppState.ingredients)
const newRecipe = computed(()=> AppState.newRecipe)
const ingredientData = {}
const ingredient = ref({
  quantity: '',
  name: '',
  id: 0
})

async function createIngredient(){
    try {
      ingredientData.recipeId = newRecipe.value.id
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
      ingredientData.quantity = ingredient.value.quantity
      ingredientData.name = ingredient.value.name
      ingredientData.id = ingredientId
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
    const confirm = await Pop.confirm("Do you really want to delete this ingredient?")
    if(!confirm){
      return
    }
    ingredientService.trashIngredient(ingredientId)
  }
  catch (error){
    Pop.toast("Unable to delete", 'error');
    logger.error("Unable to delete ingredient", error)
  }
}

function resetForm(){
    ingredient.value.quantity = ''
    ingredient.value.name = ''
    ingredient.value.id = 0
}
</script>


<template>
      <div v-if="ingredients">
        <form v-for="ingredient in ingredients" :key="ingredient.id" @submit.prevent="updateIngredient()">
          <div class="input-group mb-3">
            <input v-model="ingredient.quantity" type="text" class="form-control" :placeholder="ingredient.quantity" aria-label="Quantity">
            <input v-model="ingredient.name" type="text" class="form-control" :placeholder="ingredient.name" aria-label="Name" maxlength="250" aria-describedby="ingredientButton" name="ingredient">
            <button class="btn btn-outline-primary p-0" type="submit" id="ingredientButton"><small><i class="mdi mdi-pencil"></i></small></button>
            <button class="btn btn-outline-danger p-0" type="button" id="ingredientButton" @click="trashIngredient(ingredient.id)"><small><i class="mdi mdi-trash-can"></i></small></button>
          </div>
          <input type="number" class="d-none" v-model="ingredient.id" :placeholder="ingredient.id">
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
</template>


<style lang="scss" scoped>

</style>