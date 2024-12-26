<script lang="ts" setup>
import {ref, onMounted} from 'vue';
import {useRoute} from 'vue-router';
import {getPatient} from '@/api/PatientApi';
import type {Illness} from "@/model/illness";
import {getExamination, getExaminationForIllness} from "@/api/ExaminationApi";
import type {Examination} from "@/model/examination";
import {getIllness} from "@/api/IllnessApi";

const route = useRoute();
const examination = ref<Examination | undefined>(undefined);

const fetchDetails = async () => {
  // @ts-ignore
  const guid = route.params['guid'] as string;
  const response = await getExamination(guid);
  examination.value = response.data;
};

onMounted(() => {
  fetchDetails();
});
</script>

<template>
  <v-container v-if="examination">
    <v-row>
      <v-col cols="12">
        <h1>{{ examination.type}}</h1>
        <p>Start: {{examination.examinationTime}}</p>
      </v-col>
      <v-col cols="12" style="display: flex; gap: 1rem;">
        <v-btn color="Info">Edit</v-btn>
        <v-btn color="danger">Delete</v-btn>
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>
/* Add any necessary styles */
</style>
