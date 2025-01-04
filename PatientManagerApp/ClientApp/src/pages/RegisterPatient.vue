<template>
  <v-container>
    <v-form ref="form" v-model="valid" @submit.prevent="submitForm">
      <v-text-field
        v-model="patient.name"
        :rules="[rules.required]"
        label="Name"
        required
      ></v-text-field>
      <v-text-field
        v-model="patient.surname"
        :rules="[rules.required]"
        label="Surname"
        required
      ></v-text-field>
      <v-text-field
        v-model="patient.mbo"
        :rules="[rules.required]"
        label="MBO"
        required
      ></v-text-field>
      <v-btn :disabled="!valid" color="primary" type="submit">Register</v-btn>
    </v-form>
  </v-container>
</template>

<script lang="ts" setup>
import {ref} from 'vue';
import {useRouter} from 'vue-router';
import {createPatient} from '@/api/PatientApi';
import type {Patient} from "@/model/patient"; // Adjust the import according to your project structure

const valid = ref(false);
const patient = ref<Patient>({
  guid: "1",
  medicalHistoryGuid: "1",
  name: '',
  surname: '',
  mbo: ''
});
const rules = {
  required: (value: string) => !!value || 'Required.',
};
const router = useRouter();

const submitForm = async () => {
  if (valid.value) {
    try {
      await createPatient(patient.value);

      //@ts-ignore
      await router.push({name: 'YourPatients'});
    } catch (error) {
      console.error('Failed to create patient:', error);
    }
  }
};
</script>

<style scoped>
.v-container {
  max-width: 600px;
  margin: 3rem auto;
  padding: 1rem;
  display: flex;
  justify-content: start;
  flex-direction: column;
  height: calc(100vh - 40px);
}
</style>
