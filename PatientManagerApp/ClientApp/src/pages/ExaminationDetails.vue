<script lang="ts" setup>
import {ref, onMounted} from 'vue';
import {useRoute, useRouter} from 'vue-router';
import {
  deleteExamination,
  getExamination,
  updateExamination,
  uploadExaminationPicture
} from "@/api/ExaminationApi";
import type {Examination} from "@/model/examination";
import {ExaminationTypeProps, ExaminationTypeText} from "@/enums/ExaminationType";
import type {Illness} from "@/model/illness";
import {getIllness} from "@/api/IllnessApi";

const route = useRoute();
const router = useRouter();
const examination = ref<Examination | undefined>(undefined);
const illness = ref<Illness | undefined>(undefined);
const dialogDelete = ref(false);
const Editing = ref(false);
const selectedFile = ref<File | null>(null);

const fetchDetails = async () => {
  //@ts-ignore
  const guid = route.params['guid'] as string;
  const response = await getExamination(guid);
  examination.value = response.data;
};

const fetchIllness = async () => {
  if (!examination.value?.illness) {
    return;
  }
  const rez = await getIllness(examination.value.illness.guid);
  illness.value = rez.data;
};

const deleteItemConfirm = async () => {
  if (!examination.value) return;
  const resposn = await deleteExamination(examination.value?.guid);
  if (resposn.status !== 204) {
    console.log(resposn);
    return;
  }
  router.back();
};

const save = async () => {
  if (!examination.value) return;
  const rez = await updateExamination(examination.value?.guid!, examination.value);
  if (rez.status !== 200) {
    console.log(rez);
    return;
  }
  Editing.value = false;
  await fetchDetails();
};

const handleFileUpload = async () => {
  if (!selectedFile.value || !examination.value) return;
  const formData = new FormData();
  formData.append('file', selectedFile.value);
  await uploadExaminationPicture(examination.value.guid, formData);
  await fetchDetails();
};

onMounted(async () => {
  await fetchDetails();
  await fetchIllness();
});
</script>

<template>
  <v-container v-if="examination">
    <v-row>
      <v-col cols="12" v-if="!Editing">
        <h1>{{ ExaminationTypeText(examination.type) }}</h1>
        <p>Start: {{ examination.examinationTime }}</p>
        <!-- @vue-expect-error -->
        <p v-if="illness" @click="router.push({name: 'IllnessDetails', params: {guid: illness.guid}})">
          Examination for illness: {{ illness.name }}
        </p>
      </v-col>
      <div v-else style="width: 50%">
        <v-col cols="12" md="4" sm="6">
          <v-select v-model="examination.type" label="Examination type" :items="ExaminationTypeProps"/>
        </v-col>
        <v-col cols="12" md="8" sm="6">
          <v-text-field v-model="examination.examinationTime" label="Examination time"/>
        </v-col>
        <v-col cols="12">
          <v-file-input v-model="selectedFile" density="default"/>
          <v-btn @click="handleFileUpload">Upload Picture</v-btn>
        </v-col>
      </div>
      <v-col cols="12" style="display: flex; gap: 1rem;">
        <v-btn v-if="!Editing" color="Info" @click="Editing = true">Edit</v-btn>
        <v-btn v-else color="Info" @click="Editing = false">Cancel</v-btn>
        <v-btn v-if="Editing" color="Info" @click="save">Save</v-btn>
        <v-btn color="danger" @click="dialogDelete = true">Delete</v-btn>
      </v-col>
    </v-row>
    <v-dialog v-model="dialogDelete" max-width="500px">
      <v-card>
        <v-card-title class="text-h5">Are you sure you want to delete this item?</v-card-title>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue-darken-1" variant="text" @click="dialogDelete = false">Cancel</v-btn>
          <v-btn color="blue-darken-1" variant="text" @click="deleteItemConfirm">OK</v-btn>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <div class="mt-3" style="display: flex; height: 70vh">
      <div class="gallery">
        <image-examination v-for="img in examination.fileGuids" :img-name="img"/>
      </div>
    </div>
  </v-container>
</template>

<style scoped>
.gallery {
  flex-grow: 1;
  gap: 1rem;
  overflow-y: scroll;
  overflow-x: hidden;
  display: flex;
  justify-content: space-between;
  flex-direction: column;
}
</style>
