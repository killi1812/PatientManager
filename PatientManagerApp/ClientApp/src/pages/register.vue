<template>
  <v-container>
    <v-form ref="form" v-model="valid" lazy-validation>
      <v-text-field
        v-model="doctor.name"
        :rules="nameRules"
        label="Name"
        required
      ></v-text-field>

      <v-text-field
        v-model="doctor.surname"
        :rules="surnameRules"
        label="Surname"
        required
      ></v-text-field>

      <v-text-field
        v-model="doctor.email"
        :rules="emailRules"
        label="Email"
        required
      ></v-text-field>

      <v-text-field
        v-model="doctor.phone"
        :rules="phoneRules"
        label="Phone"
        required
      ></v-text-field>

      <v-text-field
        v-model="doctor.password"
        :rules="passwordRules"
        label="Password"
        type="password"
        required
      ></v-text-field>

      <v-btn :disabled="!valid" @click="submit">Register</v-btn>
    </v-form>
  </v-container>
</template>

<script setup lang="ts">
import {ref} from 'vue'
import type {NewDoctor} from '@/model/newDoctor'
import {register} from "@/api/DoctorApi";

const valid = ref(false)
const doctor = ref<NewDoctor>({
  name: "",
  surname: "",
  email: "",
  phone: "",
  password: ""
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

const submit = async () => {
  if (valid.value) {
    // Handle form submission
    console.log('Form submitted:', doctor.value)
    const rez = await register(doctor.value)
    console.log(rez)
  }
}
</script>

<style scoped>
.v-container {
  max-width: 600px;
  margin: 0 auto;
}
</style>
