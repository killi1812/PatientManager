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
      <div class="btn-container">
        <v-btn type="submit" color="primary">Login</v-btn>
        <v-btn type="button" color="primary" @click="router.push({name:'/register'})">Register</v-btn>
      </div>
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
const router = useRouter()

const loginUser = async () => {
  try {
    const response = await login(email.value, password.value)
    console.log('Login successful:', response.data)
    authStore.setToken(response.data)
    router.replace({name: "/landingPage"})
  } catch (error) {
    console.error('Login failed:', error)
  }
}
</script>

<style scoped>
.btn-container{
  display: flex;
  justify-content: space-evenly;
}

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
