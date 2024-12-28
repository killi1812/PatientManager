<script lang="ts" setup>

import {createIllness, deleteIllness, endIllness, getIllnessesForPatient, updateIllness} from "@/api/IllnessApi";
import type {Illness} from "@/model/illness";

const props = defineProps({
  medicalHistoryGuid: {
    type: String,
    required: true,
  },
  height: Number,
})

const defaultItem = {
  guid: '',
  name: '',
  start: '',
  end: undefined,
  medicalHistoryGuid: props.medicalHistoryGuid,
  prescriptions: [],
} as Illness

const illnessList = ref<Illness[]>([])
const editedIndex = ref(-1)
const editedItem = ref<Illness>(defaultItem)
const dialog = ref(false)
const dialogDelete = ref(false)
const formTitle = ref('')
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
const close = () => {
  dialog.value = false
  nextTick(() => {
    editedItem.value = Object.assign({}, defaultItem)
    editedIndex.value = -1
  })
}

const closeDelete = () => {
  dialogDelete.value = false
  nextTick(() => {
    editedItem.value = Object.assign({}, defaultItem)
    editedIndex.value = -1
  })
}

const deleteItemConfirm =async (guid : string) => {
  await deleteIllness(editedItem.value.guid)
  await fetchIllnessList()
  closeDelete()
}

const save = async () => {
  if (editedIndex.value > -1) {
    //TODO update doens't map propperly
    await updateIllness(editedItem.value.guid, editedItem.value)
  } else {
    await createIllness(props.medicalHistoryGuid, editedItem.value)
  }
  await fetchIllnessList()
  close()
}

const search = (item: Illness) => {
  //@ts-ignore
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
        <v-dialog v-model="dialog" max-width="500px">
          <template v-slot:activator="{ props }">
            <v-btn color="primary" @click="dialog = true" v-bind:props>Add Illness</v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="text-h5">{{ formTitle }}</span>
            </v-card-title>

            <v-card-text>
              <v-container>
                <v-row>
                  <v-col cols="12" md="4" sm="6">
                    <v-text-field
                      v-model="editedItem.name"
                      label="Illness name"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" md="4" sm="6">
                    <v-text-field
                      v-model="editedItem.start"
                      label="Start date"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" md="4" sm="6">
                    <v-text-field
                      v-model="editedItem.end"
                      label="Illness end date"
                    ></v-text-field>
                  </v-col>
                </v-row>
              </v-container>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue-darken-1" variant="text" @click="close">
                Cancel
              </v-btn>
              <v-btn color="blue-darken-1" variant="text" @click="save">
                Save
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <v-dialog v-model="dialogDelete" max-width="500px">
          <v-card>
            <v-card-title class="text-h5"
            >Are you sure you want to delete this item?
            </v-card-title
            >
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue-darken-1" variant="text" @click="closeDelete"
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
      </v-toolbar>
    </template>

    <template v-slot:item.actions="{ item }">
      <v-icon v-if="item.end == undefined" size="large" @click="stop(item)"> mdi-stop</v-icon>
      <v-icon v-else></v-icon>
      <v-icon size="large" @click="search(item)"> mdi-magnify</v-icon>
      <v-icon class="me-2" size="small" @click="editItem(item)"> mdi-pencil</v-icon>
      <v-icon size="large" @click="deleteItem(item)"> mdi-delete</v-icon>
    </template>
  </v-data-table-virtual>
</template>

<style scoped>

</style>
