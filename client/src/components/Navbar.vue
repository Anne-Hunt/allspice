<script setup>
import { computed, onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';
import { searchService } from '../services/SearchService.js';

const theme = ref(loadState('theme') || 'light')
const searchingQuery = ref('')
const searchTerms= computed(() => AppState.searchTerms)


async function search(){
	try {
		await searchService.searchTerms(searchingQuery)
		this.router.push({ name: "Home", params: { query: searchingQuery.value } })
		searchingQuery.value = ''
	} catch (error) {
		logger.error('search failure!!', error)
		Pop.toast("Unable to search at the moment sir", 'error')
	}
}
function toggleTheme() {
  theme.value = theme.value == 'light' ? 'dark' : 'light'
  document.documentElement.setAttribute('data-bs-theme', theme.value)
  saveState('theme', theme.value)
}

onMounted(() => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
})
</script>

<template>
  <nav class="navbar navbar-expand-sm px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center fontfix">
        <i class="mdi mdi-noodles fs-1 text-light"></i>
      </div>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase fontfix">
            About
          </router-link>
        </li>
      </ul>
      <div class="d-flex align-items-center">
        <div class="input-group input-group-sm">
          <input v-model="searchingQuery" type="text" class="form-control shadow" aria-label="search-input" aria-describedby="search-input-label">
          <span class="input-group-text" id="search-input-label"><i class="mdi mdi-magnify fs-5 text-success"></i></span>
</div>
      </div>
      <!-- LOGIN COMPONENT HERE -->
      <div>
        <button class="btn text-warning" @click="toggleTheme"
          :title="`Enable ${theme == 'light' ? 'dark' : 'light'} theme.`">
          <i class="mdi" :class="theme == 'light' ? 'mdi-lightbulb fs-1 text-light fontfix' : 'mdi-lightbulb-outline fs-1'"></i>
        </button>
      </div>
      <Login />
    </div>
  </nav>
</template>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.fontfix{
  text-shadow: 1px 1px 4px black;
}

@media screen and (min-width: 576px) {
  nav {
    height: 64px;
  }
}
</style>
