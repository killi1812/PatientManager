<script lang="ts" setup>
import {ref, onMounted} from 'vue';
import {useRoute} from 'vue-router';
import {getPatient} from '@/api/PatientApi';
import type {Illness} from "@/model/illness";
import {getExaminationForIllness} from "@/api/ExaminationApi";
import type {Examination} from "@/model/examination";
import {getIllness} from "@/api/IllnessApi";

const route = useRoute();
const illness = ref<Illness | undefined>(undefined);
const examinations = ref<Examination[]>([]);

const fetchIllnessDetails = async () => {
  // @ts-ignore
  const guid = route.params['guid'] as string;
  const response = await getIllness(guid);
  illness.value = response.data;
};

watch([illness], () => {
  if (illness.value) {
    fetchExaminations()
  }
}, {immediate: true, deep: true})

const fetchExaminations = async () => {
  if (!illness.value) {
    return
  }
  const response = await getExaminationForIllness(illness.value.guid)
  examinations.value = response.data
}

onMounted(() => {
  fetchIllnessDetails();
});
</script>

<template>
  <v-container v-if="illness">
    <v-row>
      <v-col cols="12">
        <h1>{{ illness.name }}</h1>
        <p>Start: {{illness.start}}</p>
        <p v-if="illness.end">End: {{illness.end}}</p>
        <p v-else>Ongoing</p>
      </v-col>
      <v-col cols="12" style="display: flex; gap: 1rem;">
        <v-btn v-if="!illness.end" color="Primary">Stop</v-btn>
        <v-btn color="Info">Edit</v-btn>
        <v-btn color="danger">Delete</v-btn>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12">
        <PrescriptionList :prescriptions="illness.prescriptions" :height="500"/>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12">
        <ExaminationList :illness-guid="illness.guid" :medical-history-guid="illness.medicalHistoryGuid" :height="500"/>
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
