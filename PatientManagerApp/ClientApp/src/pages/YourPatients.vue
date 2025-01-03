<script lang="ts" setup>
import {ref, onMounted} from 'vue';
import {getYourPatients} from '@/api/PatientApi';
import {useRouter} from "vue-router";
import type {Patient} from "@/model/patient";

const loading = ref(false);
const patients = ref([]);
const router = useRouter();
const headers = [
  {title: 'Name', key: 'name'},
  {title: 'Surname', key: 'surname'},
  {title: 'Mbo', key: 'mbo'},
  {title: 'Actions', key: 'actions', sortable: false},
];

const fetchPatients = async () => {
  loading.value = true;
  try {
    const response = await getYourPatients();
    patients.value = response.data;
  } catch (error) {
    console.error('Failed to fetch patients:', error);
  } finally {
    loading.value = false;
  }
};

const viewPatient = (patient: Patient) => {
  //@ts-ignore
  router.push({name: 'PatientDetails', params: {guid: patient.guid}});
};

const editPatient = (patient: Patient) => {
  // Implement edit patient logic
};

const deletePatient = (patient: Patient) => {
  // Implement delete patient logic
};

onMounted(async () => {
  await fetchPatients()
})

</script>
<template>
  <v-container>
    <v-data-table
      :headers="headers"
      :items="patients"
      :loading="loading"
      class="elevation-1"
    >
      <template v-slot:top>
        <v-toolbar flat>
          <v-toolbar-title>Your Patients</v-toolbar-title>
          <v-spacer></v-spacer>
        </v-toolbar>
      </template>
      <template v-slot:item.actions="{ item }">
        <v-icon small @click="viewPatient(item)">mdi-eye</v-icon>
        <v-icon small @click="editPatient(item)">mdi-pencil</v-icon>
        <v-icon small @click="deletePatient(item)">mdi-delete</v-icon>
      </template>
    </v-data-table>
  </v-container>
</template>

<style scoped>
/* Add any custom styles here */
</style>
