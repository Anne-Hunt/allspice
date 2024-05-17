<script setup>
import { ref } from 'vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { recipeService } from '../services/RecipeService.js';

const recipe = ref({
    ingredient: [],
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
</script>


<template>
<form @submit.prevent="createRecipe()">
    <label for="ingredient">Ingredients:</label>
    <input v-modal="recipe.ingredient" type="text" name="ingredient" id="ingredient" maxlength="250">
    <button type="submit">SUBMIT</button>
</form>
</template>


<style lang="scss" scoped>

</style>