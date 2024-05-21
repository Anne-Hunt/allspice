<script setup>
import { Recipe } from '../models/Recipe.js';


defineProps({recipe: Recipe})
</script>


<template>
<div class="modal fade" :id="`modal-${recipe?.id}`" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="exampleModalLabel">{{ recipe?.title }}</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        ...
      </div>
    </div>
  </div>
</div>

</template>


<style lang="scss" scoped>

</style>