<script setup>
import Navbar from './components/Navbar.vue'


</script>

<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer class="text-end p-2">
    <button class="btn btn-success rounded-circle" type="button"  data-bs-toggle="modal" data-bs-target="recipeFormModal"><i class="mdi mdi-plus fs-1 text-light"></i></button>
  </footer>
</template>

<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}
</style>
