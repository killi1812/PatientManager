<script lang="ts" setup>
import {ref, onMounted} from 'vue';
import {useRoute} from 'vue-router';
import {deleteExamination, getExamination, updateExamination} from "@/api/ExaminationApi";
import type {Examination} from "@/model/examination";
import {ExaminationTypeProps, ExaminationTypeText} from "@/enums/ExaminationType";
import type {Illness} from "@/model/illness";
import {getIllness} from "@/api/IllnessApi";

const route = useRoute();
const router = useRouter();
const examination = ref<Examination | undefined>(undefined);
const illness = ref<Illness | undefined>(undefined)
const dialogDelete = ref(false)
const Editing = ref(false)

const fetchDetails = async () => {
  // @ts-ignore
  const guid = route.params['guid'] as string;
  const response = await getExamination(guid);
  examination.value = response.data;
};

const fetchIllness = async () => {
  if (!examination.value?.illness) {
    return
  }
  const rez = await getIllness(examination.value.illness.guid)
  illness.value = rez.data
}


const deleteItemConfirm = async () => {
  if (!examination.value)
    return
  const resposn = await deleteExamination(examination.value?.guid)
  if (resposn.status !== 204) {
    console.log(resposn)
    return
  }
  //TODO: router.back doesn't work
  router.back();
}

const save = async () => {
  if (!examination.value)
    return

  const rez = await updateExamination(examination.value?.guid!, examination.value);
  if (rez.status !== 200) {
    console.log(rez)
    return
  }
  Editing.value = false
  await fetchDetails()
}

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
        <p v-if="illness" @click=" router.push({name: 'IllnessDetails', params: {guid: illness.guid}}) ">Examination for
          illness: {{ illness.name }}</p>
      </v-col>
      <div v-else style="width: 50%">
        <v-col cols="12" md="4" sm="6">
          <v-select
            v-model="examination.type"
            label="Examinaiton type"
            :items="ExaminationTypeProps"
          />
        </v-col>
        <v-col cols="12" md="8" sm="6">
          <v-text-field
            v-model="examination.examinationTime"
            label="Examination time"
          ></v-text-field>
        </v-col>
      </div>
      <v-col cols="12" style="display: flex; gap: 1rem;">
        <v-btn v-if="!Editing" color="Info" @click="Editing = true">Edit</v-btn>
        <v-btn v-else color="Info" @click="Editing = false">cancle</v-btn>
        <v-btn v-if="Editing" color="Info" @click="save">save</v-btn>
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
