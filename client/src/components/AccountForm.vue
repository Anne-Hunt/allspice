<script setup>
import { computed, onMounted, ref } from 'vue';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { AppState } from '../AppState.js';
import { accountService } from '../services/AccountService.js';

const account = computed(()=>AppState.account)

const accountData = ref({
    name: '',
    picture: '',
    coverImg: '',
    // email: ''
})

async function updateAccount(){
    try {
        const updateAccount = {}
        updateAccount.name = accountData.value.name
        updateAccount.coverImg = accountData.value.coverImg
        updateAccount.picture = accountData.value.picture
        // updateAccount.email = accountData.value.email
      await accountService.updateAccount(updateAccount)
      resetForm()
    }
    catch (error){
      Pop.error("Unable to update account at this time", 'error');
      logger.log("failed to update account", error)
    }
}

function resetForm(){
    accountData.value.name = ''
    accountData.value.picture = ''
    accountData.value.email = ''
    accountData.value.coverImg = ''
}

function values(){
    accountData.value.name = AppState.account?.name
    accountData.value.picture = AppState.account?.picture
    accountData.value.coverImg = AppState.account?.coverImg
    accountData.value.email = AppState.account?.email
}

onMounted(()=>{
    values()
})
</script>


<template>
<form @submit.prevent="updateAccount()">
    <div class="mb-3">
        <label for="name">Name</label>
        <input type="text" v-model="accountData.name" class="w-100">
    </div>
    <div class="mb-3">
        <label for="picture">Picture</label>
        <input type="text" v-model="accountData.picture" class="w-100">
    </div>
    <div class="mb-3">
        <label for="coverImage">Cover Image</label>
        <input type="text" v-model="accountData.coverImg" class="w-100">
    </div>
    <!-- <div class="mb-3">
        <label for="Email">Email</label>
        <input type="text" v-model="accountData.email" class="w-100">
    </div> -->

    <div class="d-flex justify-content-end">
        <button class="btn btn-dark text-light text-end" type="submit">Update Account</button>
    </div>
</form>
</template>


<style lang="scss" scoped>

</style>