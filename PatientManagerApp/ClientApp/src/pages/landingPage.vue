<template>
  <v-container>
    <v-row justify="center">
      <v-col cols="12" md="8">
        <v-text-field
          v-model="searchQuery"
          label="Search patients"
          @input="searchPatients"
          class="mx-auto"
        ></v-text-field>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12">
        <v-data-table
          :headers="headers"
          :items="patients"
          class="elevation-1"
        >
          <template v-slot:item="{ item }">
            <tr @click="navigeteToPatient(item)">
              <td>{{ item.name }}</td>
              <td>{{ item.surname }}</td>
            </tr>
          </template>
        </v-data-table>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts" setup>
import { ref } from 'vue';
import { getPatients } from '@/api/PatientApi';
import type { Patient } from '@/model/patient';

const searchQuery = ref('');
const patients = ref<Patient[]>([]);
const router = useRouter();
const headers = [
  { title: 'Name', key: 'name' },
  { title: 'Surname', key: 'surname' },
];

const searchPatients = async () => {
  if (searchQuery.value.trim() !== '') {
    const response = await getPatients(searchQuery.value);
    patients.value = response.data;
  } else {
    patients.value = [];
  }
};

const navigeteToPatient = (patient: Patient) => {
  router.push({name: 'PatientDetails', params: {guid: patient.guid}});
};
</script>

<style scoped>
.mx-auto {
  margin-left: auto;
  margin-right: auto;
}
</style>
