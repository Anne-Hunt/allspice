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
    <div class="modal-dialog modal-dialog-scrollable modal-xl">
      <div class="modal-content">
        <div class="modal-header justify-content-between">
          <h3 v-if="!recipe" class="modal-title fs-5" id="modalFormLabel">Create</h3>
          <h3 v-else>{{ recipe?.title }}</h3>
          <button v-if="!recipe" type="button" class="btn btn-success rounded" data-bs-dismiss="modal" aria-label="Close"
            @click="closeAndReset()">Save & Close</button>
          <button v-else type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"
            @click="closeAndReset()"></button>
        </div>
        <div class="row modal-body m-0 p-0">
          <div v-if="AppState.activeRecipe" class="row">
            <div class="col-12">
              <RecipeDetails />
            </div>
          </div>
          <div v-else class="row">
            <div v-if="AppState.newRecipe" class="rounded-start-bottom imgCard col-md-4"
              :style="{backgroundImage: `url(${AppState.newRecipe.img})`} "></div>
            <div class="col-md-4 col-10 my-2">
              <h5>Recipe</h5>
              <RecipeForm />
            </div>
            <div v-if="AppState.newRecipe" class="col-md-4 col-10 my-2">
              <h5>Ingredients</h5>
              <IngredientForm />
            </div>
            </div>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.imgCard{
    background-position: center;
    background-size:cover;
    height: 100dvh;
}
</style>