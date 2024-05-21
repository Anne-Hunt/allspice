<script setup>
import { ref } from 'vue';

const image = ref({
    link: ''
})
</script>


<template>
<div>
<img :src="image.link" alt="">
</div>
<RecipeForm/>
<IngredientsForm/>
</template>


<style lang="scss" scoped>

</style>