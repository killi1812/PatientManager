<template>
  <v-container>
    <v-form ref="form" v-model="valid" lazy-validation>
      <v-text-field
        v-model="name"
        :rules="nameRules"
        label="Name"
        required
      ></v-text-field>

      <v-text-field
        v-model="surname"
        :rules="surnameRules"
        label="Surname"
        required
      ></v-text-field>

      <v-text-field
        v-model="email"
        :rules="emailRules"
        label="Email"
        required
      ></v-text-field>

      <v-text-field
        v-model="phone"
        :rules="phoneRules"
        label="Phone"
        required
      ></v-text-field>

      <v-text-field
        v-model="password"
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
import { ref } from 'vue'

const valid = ref(false)
const name = ref('')
const surname = ref('')
const email = ref('')
const phone = ref('')
const password = ref('')

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

const submit = () => {
  if (valid.value) {
    // Handle form submission
    console.log('Form submitted:', { name: name.value, surname: surname.value, email: email.value, phone: phone.value, password: password.value })
  }
}
</script>

<style scoped>
.v-container {
  max-width: 600px;
  margin: 0 auto;
}
</style>
