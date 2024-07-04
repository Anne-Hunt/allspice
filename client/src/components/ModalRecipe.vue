<script setup>
import { computed, watch } from 'vue';
import { AppState } from '../AppState.js';
import { Recipe } from '../models/Recipe.js';
import IngredientsDetails from './IngredientsDetails.vue';
import RecipeDetails from './RecipeDetails.vue';
import { recipeService } from '../services/RecipeService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

const active = computed(()=>AppState.activeRecipe)

defineProps({recipe: Recipe})



// watch(
//   () => AppState.activeRecipe,
//    => {
//     async function getIngredients(){
//       await: recipeService.getIngredientsByRecipe(active.value.id)
// }});
</script>


<template>
<div class="modal fade" :id="`modal-${recipe?.id}`" tabindex="-1" aria-labelledby="establishedRecipeModal" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="establishedRecipeModal">{{ recipe?.title }}</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        
        <div v-if="AppState.activeRecipe" class="row">
          <img class="col-12 col-md-5" :src="recipe?.img">
          <div class="col-12 col-md-4">
            <RecipeDetails/>
          </div>
          <div class="col-12 col-md-3">
            <IngredientsDetails/>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>

</template>


<style lang="scss" scoped>

</style>