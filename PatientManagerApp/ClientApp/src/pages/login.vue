<template>
  <v-container class="login-container">
    <v-form @submit.prevent="login">
      <v-text-field
          v-model="username"
          label="Username"
          required
      ></v-text-field>
      <v-text-field
          v-model="password"
          label="Password"
          type="password"
          required
      ></v-text-field>
      <v-btn type="submit" color="primary">Login</v-btn>
    </v-form>
  </v-container>
</template>

<script setup lang="ts">
import {ref} from 'vue'
import {Login} from "@/api/DoctorApi";

const username = ref('')
const password = ref('')

const login = async () => {
  try {
    const response = await Login(username.value, password.value)
    console.log('Login successful:', response.data)
  } catch (error) {
    console.error('Login failed:', error)
  }
}
</script>

<style scoped>
.login-container {
  max-width: 400px;
  margin: 0 auto;
  padding: 1rem;
  display: flex;
  justify-content: center;
  flex-direction: column;
  height: calc(100vh - 40px);
}
</style>
