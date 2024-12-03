<template>
  <v-container class="login-container">
    <v-form @submit.prevent="loginUser">
      <v-text-field
        v-model="email"
        label="email"
        required
        type="email"
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
import {login} from "@/api/DoctorApi";
import {useAuthStore} from "@/stores/auth";

const email = ref('')
const password = ref('')
const authStore = useAuthStore()
const routher = useRouter()

const loginUser = async () => {
  try {
    const response = await login(email.value, password.value)
    console.log('Login successful:', response.data)
    authStore.setToken(response.data)
    routher.replace({name: "/landingPage"})
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
