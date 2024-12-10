<template>
  <div>
    <input v-model="searchQuery" @input="searchPatients" placeholder="Search patients..." />
    <ul>
      <li v-for="patient in patients" :key="patient.guid">
        {{ patient.name }} {{ patient.surname }}
      </li>
    </ul>
  </div>
</template>

<script lang="ts" setup>
import { getPatients } from '@/api/PatientApi';
import type { Patient } from '@/model/patient';

    const searchQuery = ref('');
    const patients = ref<Patient[]>([]);

    const searchPatients = async () => {
      if (searchQuery.value.trim() !== '') {
        const response = await getPatients(searchQuery.value);
        patients.value = response.data;
      } else {
        patients.value = [];
      }
    };
</script>

<style scoped>
input {
  width: 100%;
  padding: 8px;
  margin-bottom: 16px;
  box-sizing: border-box;
}

ul {
  list-style-type: none;
  padding: 0;
}

li {
  padding: 8px;
  border-bottom: 1px solid #ccc;
}
</style>
