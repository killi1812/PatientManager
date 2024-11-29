<template>
  <v-app-bar app>
    <v-toolbar-title>
      <v-btn  to="/examinations">Upcoming Examinations</v-btn>
      <v-btn  to="/patients">Your Patients</v-btn>
      <v-btn  to="/register-patient">Register a Patient</v-btn>
    </v-toolbar-title>

    <v-spacer></v-spacer>

    <v-text-field
      v-model="search"
      append-icon="mdi-magnify"
      label="Search"
      single-line
      hide-details
    ></v-text-field>

    <v-spacer></v-spacer>

    <v-toolbar-title>
      {{ username }}
    </v-toolbar-title>

    <v-btn  @click="logout">Logout</v-btn>
  </v-app-bar>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useAuthStore } from '@/stores/auth'

const search = ref('')
const authStore = useAuthStore()
const username = authStore.username

const logout = () => {
  authStore.logout()
  // Redirect to login page or perform other logout actions
}
</script>

<style scoped>
.v-toolbar-title {
  margin: 0 16px;
}
</style>
