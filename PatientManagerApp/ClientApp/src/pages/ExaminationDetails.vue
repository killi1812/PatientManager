<script lang="ts" setup>
import {ref, onMounted} from 'vue';
import {useRoute} from 'vue-router';
import {deleteExamination, getExamination} from "@/api/ExaminationApi";
import type {Examination} from "@/model/examination";
import {ExaminationTypeProps, ExaminationTypeText} from "@/enums/ExaminationType";

const route = useRoute();
const router = useRouter();
const examination = ref<Examination | undefined>(undefined);
const dialogDelete = ref(false)

const fetchDetails = async () => {
  // @ts-ignore
  const guid = route.params['guid'] as string;
  const response = await getExamination(guid);
  examination.value = response.data;
};

const deleteItemConfirm = async () => {
  if (!examination.value)
    return
  const resposn = await deleteExamination(examination.value?.guid)
  console.log(resposn)
  debugger
  if (resposn.status !== 204) {
    console.log(resposn)
    return
  }
  router.back();
}

onMounted(() => {
  fetchDetails();
});
</script>

<template>
  <v-container v-if="examination">
    <v-row>
      <v-col cols="12">
        <h1>{{ ExaminationTypeText(examination.type) }}</h1>
        <p>Start: {{ examination.examinationTime }}</p>
      </v-col>
      <v-col cols="12">
        <v-select :items="ExaminationTypeProps"/>
      </v-col>
      <v-col cols="12" style="display: flex; gap: 1rem;">
        <v-btn color="Info">Edit</v-btn>
        <v-btn color="danger" @click="dialogDelete = true">Delete</v-btn>
      </v-col>
    </v-row>
    <v-dialog v-model="dialogDelete" max-width="500px">
      <v-card>
        <v-card-title class="text-h5"
        >Are you sure you want to delete this item?
        </v-card-title
        >
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue-darken-1" variant="text" @click="dialogDelete = false"
          >Cancel
          </v-btn
          >
          <v-btn
            color="blue-darken-1"
            variant="text"
            @click="deleteItemConfirm"
          >OK
          </v-btn
          >
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<style scoped>
/* Add any necessary styles */
</style>
