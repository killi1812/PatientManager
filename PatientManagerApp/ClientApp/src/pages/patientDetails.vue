<script lang="ts" setup>
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { getPatient } from '@/api/PatientApi';
import type { Patient } from '@/model/patient';

const route = useRoute();
const patient = ref<Patient | undefined>(undefined);

const fetchPatientDetails = async () => {
  const guid = route.params.guid as string;
  const response = await getPatient(guid);
  patient.value = response.data;
};

onMounted(() => {
  fetchPatientDetails();
});
</script>

<template>
  <v-container v-if="patient">
    <v-row>
      <v-col cols="12">
        <h1>{{ patient.name }} {{ patient.surname }}</h1>
        <p>MBO: {{ patient.mbo }}</p>
      </v-col>
    </v-row>
  </v-container>
  <v-container v-else>
    <v-row>
      <v-col cols="12">
        <v-progress-circular indeterminate></v-progress-circular>
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>
/* Add any necessary styles */
</style>
