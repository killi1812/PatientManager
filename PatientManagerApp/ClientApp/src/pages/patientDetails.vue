<script lang="ts" setup>
import {ref, onMounted} from 'vue';
import {useRoute} from 'vue-router';
import {downloadCsv, getPatient} from '@/api/PatientApi';
import {getAllExaminations} from "@/api/ExaminationApi";
import type {Patient} from '@/model/patient';
import type {Examination} from "@/model/examination";

const route = useRoute();
const patient = ref<Patient | undefined>(undefined);
const examinations = ref<Examination[]>([]);

const fetchPatientDetails = async () => {
  //@ts-ignore
  const guid = route.params['guid'] as string;
  const response = await getPatient(guid);
  patient.value = response.data;
};

watch([patient],() => {
  if (patient.value) {
    fetchExaminations()
  }
}, {immediate: true, deep: true})

const fetchExaminations = async () => {
  if (!patient.value) {
    return
  }
  const response = await getAllExaminations(patient.value.medicalHistoryGuid)
  examinations.value = response.data
}

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
        <v-btn @click="downloadCsv(patient.guid)">Download Medical History</v-btn>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12">
        <IllnessList :medicalHistoryGuid="patient.medicalHistoryGuid" :height="500"/>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12">
        <ExaminationList  :medicalHistoryGuid="patient.medicalHistoryGuid" :examinations="examinations" :height="500"/>
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
