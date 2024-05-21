<script setup>
import { ref } from 'vue';


const modalId = ref({
  id: ''
})
</script>

<template>
  <header>
  </header>
  <main>
    <router-view />
  </main>
  <footer class="text-end p-2 sticky-bottom">
    <button class="btn btn-success rounded-circle" type="button"  data-bs-toggle="modal" :data-bs-target="modalId" @click="modalId.id = 'create'"><i class="mdi mdi-plus fs-3 text-light"></i></button>
  </footer>
  <ModalWrapper :id="modalId.id"/>
</template>

<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}
</style>
