<script setup>
import { AppState } from '../AppState.js';


</script>


<template>
<div class="modal fade" tabindex="-1" aria-labelledby="modalFormLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="modalFormLabel">Create</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="row modal-body m-0 p-0">
          <img class="img-fluid rounded-start col-md-4" :src="AppState.newRecipe?.img">
          <div class="col-md-4 col-10 my-2"><RecipeForm/></div>
          <div v-if="AppState.newRecipe" class="col-md-4 col-10 my-2"><IngredientForm/></div>
      </div>
    </div>
    
  </div>
</div>

</template>


<style lang="scss" scoped>

</style>