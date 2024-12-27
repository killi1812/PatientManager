<script lang="ts" setup>

import {deleteExamination} from "@/api/ExaminationApi";
import type {Examination} from "@/model/examination";

const props = defineProps({
  examinations: {
    type: Array<Examination>,
    required: true,
  },
  height: Number,
})

const defaultItem: Examination = {
  examinationTime: "",
  guid: "",
  illness: [],
  type: 0
} as Examination
const router = useRouter()
const editedIndex = ref(-1)
const editedItem = ref<Examination>(defaultItem)
const dialog = ref(false)
const dialogDelete = ref(false)
const activeGroupBy = ref([])
const groupByType = {
  key: 'type',
  order: 'asc',
}

const headers = [
  {title: 'Type', key: 'type'},
  {title: 'Time', key: 'examinationTime'},
  {title: 'Actions', key: 'actions', sortable: false},
]


const editItem = (item: Examination) => {
  editedIndex.value = props.examinations.indexOf(item)
  editedItem.value = Object.assign({}, item)
  dialog.value = true
}

const deleteItem = (item: Examination) => {
  editedIndex.value = props.examinations.indexOf(item)
  editedItem.value = Object.assign({}, item)
  dialogDelete.value = true
}

const closeDelete = () => {
  dialogDelete.value = false
  nextTick(() => {
    editedItem.value = Object.assign({}, defaultItem)
    editedIndex.value = -1
  })
}

const deleteItemConfirm = async (guid: string) => {
  await deleteExamination(editedItem.value.guid)
  //TODO: if there is a delete there then i  can't just send whole array but rather refetch it everytime delete happens
  //props.examinations = props.examinations.filter(e => e.guid !== editedItem.value.guid)
  closeDelete()
}

const search = (item: Examination) => {
  //@ts-ignore
  router.push({name: 'ExaminationDetails', params: {guid: item.guid}})
}

</script>

<template>
  <v-data-table-virtual
    :group-by="activeGroupBy"
    :headers="headers"
    :items="examinations"
    :height="height ?? 400"
    item-value="guid"
    density="compact"
  >
    <template v-slot:top>
      <v-toolbar flat density="compact">
        <v-toolbar-title>Examinations</v-toolbar-title>
        <v-divider class="mx-4" inset vertical></v-divider>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="activeGroupBy = [groupByType!]">Group by type
        </v-btn>
        <v-btn color="primary" @click="activeGroupBy=[]">Clear group by</v-btn>
        <v-btn color="primary" @click="dialog = true">Add Examination</v-btn>
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
      <v-icon size="large" @click="search(item)"> mdi-magnify</v-icon>
      <v-icon class="me-2" size="small" @click="editItem(item)"> mdi-pencil</v-icon>
      <v-icon size="large" @click="deleteItem(item)"> mdi-delete</v-icon>
    </template>
  </v-data-table-virtual>
</template>

<style scoped>

</style>
