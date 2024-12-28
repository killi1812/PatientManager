<template>
  <v-app-bar app>
    <v-btn to="/examinations">Upcoming Examinations</v-btn>
    <v-btn to="/patients">Your Patients</v-btn>
    <v-btn to="/register-patient">Register a Patient</v-btn>
    <v-spacer></v-spacer>
    {{ username }}
    <v-btn @click="logout">Logout</v-btn>
  </v-app-bar>
</template>

<script setup lang="ts">
import {ref} from 'vue'
import {useAuthStore} from '@/stores/auth'

const router = useRouter()
const authStore = useAuthStore()
const username = authStore.username

const logout = () => {
  authStore.logout()
  router.replace({path: '/login'})
}
</script>

<style scoped>
.v-toolbar-title {
  margin: 0 16px;
}
</style>
