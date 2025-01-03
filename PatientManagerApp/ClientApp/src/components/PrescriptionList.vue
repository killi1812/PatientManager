<script lang="ts" setup>
import type {Prescription} from "@/model/prescription";
import {
  createPrescription,
  deletePrescription,
  getPrescriptionsForIllness,
  updatePrescription
} from "@/api/Prescriptions";
import {updateExamination} from "@/api/ExaminationApi";

const props = defineProps({
  illnessGuid: {
    type: String,
    required: true,
  },
  medicalHistoryGuid: {
    type: String,
    required: true,
  },
  height: Number,
})

const defaultValue: Prescription = {
  guid: "1",
  name: "",
  date: new Date().toLocaleDateString("us"),
  illnessGuid: props.illnessGuid,
  medicalHistoryGuid: props.medicalHistoryGuid,
}
const prescriptions = ref<Prescription[]>([])
const editedIndex = ref(-1)
const editedItem = ref<Prescription>(defaultValue)
const dialog = ref(false)
const activeGroupBy = ref([])
const loading = ref(false)
const groupByName = {
  key: 'name',
  order: 'asc',
}

const headers = [
  {title: 'Name', key: 'name'},
  {title: 'Date', key: 'date'},
  {title: 'Actions', key: 'actions', sortable: false},
]

const fetchPrescriptions = async () => {
  loading.value = true
  try {
    const rez = await getPrescriptionsForIllness(props.illnessGuid)
    prescriptions.value = rez.data
  } finally {
    loading.value = false
  }
}

const editItem = (item: Prescription) => {
  editedIndex.value = prescriptions.value.indexOf(item)
  editedItem.value = Object.assign({}, item)
  dialog.value = true
}

const deleteItem = async (item: Prescription) => {
  await deletePrescription(item.guid)
  await fetchPrescriptions()
}

const save = async () => {

  if (editedIndex.value > -1) {
    await updatePrescription(editedItem.value.guid, editedItem.value)
  } else {
    await createPrescription(editedItem.value)
  }

  await fetchPrescriptions()
  dialog.value = false
}

onMounted(async () => {
  await fetchPrescriptions()
})

</script>

<template>
  <v-data-table-virtual
    :group-by="activeGroupBy"
    :headers="headers"
    :items="prescriptions"
    :height="height ?? 400"
    item-value="guid"
    density="compact"
    :loading="loading"
  >
    <template v-slot:top>
      <v-toolbar flat density="compact">
        <v-toolbar-title>Prescriptions</v-toolbar-title>
        <v-divider class="mx-4" inset vertical></v-divider>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="activeGroupBy = [groupByName!]">Group by name
        </v-btn>
        <v-btn color="primary" @click="activeGroupBy=[]">Clear group by</v-btn>
        <v-dialog v-model="dialog" max-width="500px">
          <template v-slot:activator="{ props }">
            <v-btn color="primary" @click="dialog = true" v-bind:props>Add Prescription</v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="text-h5">Add new Examination</span>
            </v-card-title>

            <v-card-text>
              <v-container>
                <v-row>
                  <v-col cols="12" md="4" sm="6">
                    <v-text-field
                      v-model="editedItem.name"
                      label="Name"
                    />
                  </v-col>
                  <v-col cols="12" md="8" sm="6">
                    <v-text-field
                      v-model="editedItem.date"
                      label="Examination time"
                    ></v-text-field>
                  </v-col>
                </v-row>
              </v-container>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue-darken-1" variant="text" @click="dialog = false">
                Cancel
              </v-btn>
              <v-btn color="blue-darken-1" variant="text" @click="save">
                Save
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>
    </template>
    <template v-slot:group-header="{ item, columns, toggleGroup, isGroupOpen }">
      <tr>
        <td :colspan="columns.length">
          <v-btn
            :icon="isGroupOpen(item) ? '$expand' : '$next'"
            size="small"
            variant="text"
            @click="toggleGroup(item)"
          ></v-btn>
          {{ item.value }}
        </td>
      </tr>
    </template>
    <template v-slot:item.actions="{ item }">
      <v-icon class="me-2" size="small" @click="editItem(item)"> mdi-pencil</v-icon>
      <v-icon size="large" @click="deleteItem(item)"> mdi-delete</v-icon>
    </template>
  </v-data-table-virtual>
</template>

<style scoped>

</style>
