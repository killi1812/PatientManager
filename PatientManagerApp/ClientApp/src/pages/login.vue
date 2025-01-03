<template>
  <v-container class="login-container">
    <v-form @submit.prevent="loginUser" v-model="valid">
      <v-text-field
        v-model="email"
        label="email"
        required
        :rules="emailRules"
        type="email"
      ></v-text-field>
      <v-text-field
        v-model="password"
        label="Password"
        :rules="passwordRules"
        type="password"
        required
      ></v-text-field>
      <div class="btn-container">
        <v-btn type="submit" color="primary" :disabled="!valid" :loading="loading">Login</v-btn>
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
const valid = ref(false)
const loading = ref(false)

const loginUser = async () => {
  loading.value = true
  try {
    const response = await login(email.value, password.value)
    authStore.setToken(response.data.token)
    authStore.setUsername(email.value)
    //@ts-ignore
    await router.replace({name: "Home"})
  } catch (error) {
    console.error('Login failed:', error)
  } finally {
    loading.value = false
  }
}

const emailRules = [
  (v: string) => !!v || 'Email is required',
  (v: string) => /.+@.+\..+/.test(v) || 'Email must be valid',
]

const passwordRules = [
  (v: string) => !!v || 'Password is required',
  (v: string) => (v && v.length >= 6) || 'Password must be at least 6 characters',
]
</script>

<style scoped>
.btn-container {
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
