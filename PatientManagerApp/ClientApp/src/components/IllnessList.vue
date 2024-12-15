<script lang="ts" setup>

import {endIllness, getIllnessesForPatient} from "@/api/IllnessApi";
import type {Illness} from "@/model/illness";

const props = defineProps({
  medicalHistoryGuid: {
    type: String,
    required: true,
  },
  height: Number,
})
const illnessList = ref<Illness[]>([])
const editedIndex = ref(-1)
const editedItem = ref<Illness | undefined>(undefined)
const dialog = ref(false)
const dialogDelete = ref(false)
const router = useRouter()

const headers = [
  {title: 'Name', key: 'name'},
  {title: 'Start', key: 'start'},
  {title: 'End', key: 'end'},
  {title: 'Actions', key: 'actions', sortable: false},
]
const fetchIllnessList = async () => {
  const response = await getIllnessesForPatient(props.medicalHistoryGuid)
  illnessList.value = response.data
}

const editItem = (item: Illness) => {
  editedIndex.value = illnessList.value.indexOf(item)
  editedItem.value = Object.assign({}, item)
  dialog.value = true
}

const deleteItem = (item: Illness) => {
  editedIndex.value = illnessList.value.indexOf(item)
  editedItem.value = Object.assign({}, item)
  dialogDelete.value = true
}

const search = (item: Illness) => {
  router.push({name: 'IllnessDetails', params: {guid: item.guid}})
}
const stop = async (item: Illness) => {
  const rez = await endIllness(item.guid)
  if (rez.status === 200) {
    fetchIllnessList()
  }
}
onMounted(() => {
  fetchIllnessList()
})
</script>

<template>
  <v-data-table-virtual
    :headers="headers"
    :items="illnessList"
    :height="height ?? 400"
    item-value="guid"
    density="compact"
  >

    <template v-slot:top>
      <v-toolbar flat density="compact">
        <v-toolbar-title>Illnesses</v-toolbar-title>
        <v-divider class="mx-4" inset vertical></v-divider>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="dialog = true">Add Illness</v-btn>
      </v-toolbar>
    </template>

    <template v-slot:item.actions="{ item }">
      <v-icon size="large" @click="search(item)"> mdi-magnify</v-icon>
      <v-icon size="large" @click="stop(item)"> mdi-stop</v-icon>
      <v-icon class="me-2" size="small" @click="editItem(item)"> mdi-pencil</v-icon>
      <v-icon size="large" @click="deleteItem(item)"> mdi-delete</v-icon>
    </template>
  </v-data-table-virtual>
</template>

<style scoped>

</style>
