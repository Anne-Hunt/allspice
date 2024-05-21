<script setup>
import { AppState } from '../AppState.js';


</script>


<template>
<div class="modal fade" tabindex="-1" aria-labelledby="modalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="modalLabel">Create</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="row modal-body justify-content-center">
        <div class="col-6 card-body"><RecipeForm/></div>
        <div v-if="AppState.activeRecipe" class="col-6 card-body"><IngredientForm/></div>
      </div>
    </div>
  </div>
</div>

</template>


<style lang="scss" scoped>

</style>