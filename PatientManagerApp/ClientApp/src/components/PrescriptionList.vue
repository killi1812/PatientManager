<script lang="ts" setup>
import type {Prescription} from "@/model/prescription";

const props = defineProps({
  illnessGuid:{
    type: String,
    required: true,
  },
  height: Number,
})
const prescriptions = ref<Prescription[]>([])
const editedIndex = ref(-1)
const editedItem = ref<Prescription | undefined>(undefined)
const dialog = ref(false)
const dialogDelete = ref(false)
const activeGroupBy = ref([])
const groupByName = {
  key: 'name',
  order: 'asc',
}

const headers = [
  {title: 'Name', key: 'name'},
  {title: 'Date', key: 'date'},
  {title: 'Actions', key: 'actions', sortable: false},
]

const fetchPrescriptions = () => {
  //TODO: Write after writing prescriptions api
}

const editItem = (item: Prescription) => {
  editedIndex.value = prescriptions.value.indexOf(item)
  editedItem.value = Object.assign({}, item)
  dialog.value = true
}

const deleteItem = (item: Prescription) => {
  editedIndex.value = prescriptions.value.indexOf(item)
  editedItem.value = Object.assign({}, item)
  dialogDelete.value = true
}

</script>

<template>
  <v-data-table-virtual
    :group-by="activeGroupBy"
    :headers="headers"
    :items="prescriptions"
    :height="height ?? 400"
    item-value="guid"
    density="compact"
  >
    <template v-slot:top>
      <v-toolbar flat density="compact">
        <v-toolbar-title>Prescriptions</v-toolbar-title>
        <v-divider class="mx-4" inset vertical></v-divider>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="activeGroupBy = [groupByName!]">Group by name
        </v-btn>
        <v-btn color="primary" @click="activeGroupBy=[]">Clear group by</v-btn>
        <v-btn color="primary" @click="dialog = true">Add Prescription</v-btn>
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
