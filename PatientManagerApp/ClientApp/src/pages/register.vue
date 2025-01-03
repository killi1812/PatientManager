<template>
  <v-container>
    <v-form v-model="valid" lazy-validation @submit.prevent="submitRegister">
      <v-text-field
        v-model="doctor.Name"
        :rules="nameRules"
        label="Name"
        required
      ></v-text-field>

      <v-text-field
        v-model="doctor.Surname"
        :rules="surnameRules"
        label="Surname"
        required
      ></v-text-field>

      <v-text-field
        v-model="doctor.Email"
        :rules="emailRules"
        label="Email"
        required
      ></v-text-field>

      <v-text-field
        v-model="doctor.Phone"
        :rules="phoneRules"
        label="Phone"
        required
      ></v-text-field>

      <v-text-field
        v-model="doctor.Password"
        :rules="passwordRules"
        label="Password"
        type="password"
        required
      ></v-text-field>
      <div class="btn-container">
        <v-btn type="button" color="primary" @click="router.push({name:'Login'})">Login</v-btn>
        <v-btn type="submit" color="primary" :disabled="!valid" :loading="loading">Register</v-btn>
      </div>
    </v-form>
  </v-container>
</template>

<script setup lang="ts">
import {ref} from 'vue'
import type {NewDoctor} from '@/model/newDoctor'
import {register} from "@/api/DoctorApi";

const router = useRouter()
const loading = ref(false)
const valid = ref(false)
const doctor = ref<NewDoctor>({
  Name: "",
  Surname: "",
  Email: "",
  Phone: "",
  Password: ""
})

const nameRules = [
  (v: string) => !!v || 'Name is required',
  (v: string) => (v && v.length <= 50) || 'Name must be less than 50 characters',
]

const surnameRules = [
  (v: string) => !!v || 'Surname is required',
  (v: string) => (v && v.length <= 50) || 'Surname must be less than 50 characters',
]

const emailRules = [
  (v: string) => !!v || 'Email is required',
  (v: string) => /.+@.+\..+/.test(v) || 'Email must be valid',
]

const phoneRules = [
  (v: string) => !!v || 'Phone is required',
  (v: string) => /^\d{10}$/.test(v) || 'Phone must be 10 digits',
]

const passwordRules = [
  (v: string) => !!v || 'Password is required',
  (v: string) => (v && v.length >= 6) || 'Password must be at least 6 characters',
]

const submitRegister = async () => {
  if (valid.value) {
    loading.value = true
    try {
      const rez = await register(doctor.value)
      console.log(rez)
    } catch (e) {
      console.log(e)
    } finally {
      loading.value = true
    }
  }
}
</script>

<style scoped>

.btn-container {
  display: flex;
  justify-content: space-evenly;
}

.v-container {
  max-width: 400px;
  margin: 0 auto;
  padding: 1rem;
  display: flex;
  justify-content: center;
  flex-direction: column;
  height: calc(100vh - 40px);
}
</style>
