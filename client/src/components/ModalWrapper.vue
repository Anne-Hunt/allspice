<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import { recipeService } from '../services/RecipeService.js';
import { Modal } from 'bootstrap';

const recipe = computed(()=> AppState.activeRecipe)

function closeAndReset(){
  recipeService.resetRecipes()
  Modal.getOrCreateInstance('#recipeModal').hide
}
</script>


<template>
<div class="modal fade" id="recipeModal" tabindex="-1" aria-labelledby="modalFormLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl">
    <div class="modal-content">
      <div class="modal-header">
        <h1 v-if="!recipe" class="modal-title fs-5" id="modalFormLabel">Create</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" @click="closeAndReset()"></button>
      </div>
      <div class="row modal-body m-0 p-0">
        <div v-if="AppState.activeRecipe" class="row">
          <div class="col-12 col-md-4">
            <RecipeDetails/>
          </div>
          <div class="col-12 col-md-4">
            <IngredientsDetails/>
          </div>
        </div>
        <div v-else>
          <img v-if="AppState.newRecipe" class="img-fluid rounded-start col-md-4" :src="AppState.newRecipe?.img">
          <div class="col-md-4 col-10 my-2"><RecipeForm/></div>
          <div v-if="AppState.newRecipe" class="col-md-4 col-10 my-2"><IngredientForm/></div>
      </div>
    </div>
    </div>
  </div>
</div>

</template>


<style lang="scss" scoped>

</style>