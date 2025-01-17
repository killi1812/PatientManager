<script lang="ts" setup>

import {createExamination, getAllExaminations, getExaminationForIllness} from "@/api/ExaminationApi";
import type {Examination} from "@/model/examination";
import type {NewExaminationDto} from "@/dto/newExaminationDto";
import {ExaminationTypeProps, ExaminationTypeText} from "@/enums/ExaminationType";

const props = defineProps({
 medicalHistoryGuid:{
    type: String,
    required: true,
  },
  illnessGuid:String,
  height: Number,
})

const defaultItem: NewExaminationDto = {
  examinationTime: "",
  illnessGuid: props.illnessGuid,
  medicalHistoryGuid: props.medicalHistoryGuid,
  type: 0
}
const examinations = ref<Examination[]>([])
const router = useRouter()
const editedIndex = ref(-1)
const editedItem = ref<NewExaminationDto>(defaultItem)
const dialog = ref(false)
const dialogDelete = ref(false)
const activeGroupBy = ref([])
const loading = ref(false)
const groupByType = {
  key: 'type',
  order: 'asc',
}

const headers = [
  {title: 'Type', key: 'type'},
  {title: 'Time', key: 'examinationTime'},
  {title: 'Actions', key: 'actions', sortable: false},
]

const fetchExaminations = async () => {
  loading.value = true
  try{
    if (props.illnessGuid) {
      const rez = await getExaminationForIllness(props.illnessGuid)
      examinations.value = rez.data
      return
    }
    const rez = await getAllExaminations(props.medicalHistoryGuid)
    examinations.value = rez.data
  }
  catch (e){

  } finally {
    loading.value = false
  }

}

const closeDelete = () => {
  dialogDelete.value = false
  nextTick(() => {
    editedItem.value = Object.assign({}, defaultItem)
    editedIndex.value = -1
  })
}

const save = async () => {
  await createExamination(editedItem.value)
  dialog.value = false
  await fetchExaminations()
}

const search = (item: Examination) => {
  //@ts-ignore
  router.push({name: 'ExaminationDetails', params: {guid: item.guid}})
}

onMounted(async () => {
  await fetchExaminations()
})

</script>

<template>
  <v-data-table-virtual
    :group-by="activeGroupBy"
    :headers="headers"
    :items="examinations"
    :height="height ?? 400"
    item-value="guid"
    density="compact"
    :loading="loading"
  >
    <template v-slot:top>
      <v-toolbar flat density="compact">
        <v-toolbar-title>Examinations</v-toolbar-title>
        <v-divider class="mx-4" inset vertical></v-divider>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="activeGroupBy = [groupByType!]">Group by type</v-btn>
        <v-btn color="primary" @click="activeGroupBy=[]">Clear group by</v-btn>
        <v-dialog v-model="dialog" max-width="500px">
          <template v-slot:activator="{ props }">
            <v-btn color="primary" @click="dialog = true" v-bind:props>Add Examination</v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="text-h5">Add new Examination</span>
            </v-card-title>

            <v-card-text>
              <v-container>
                <v-row>
                  <v-col cols="12" md="4" sm="6">
                    <v-select
                      v-model="editedItem.type"
                      label="Examinaiton type"
                      :items="ExaminationTypeProps"
                    />
                  </v-col>
                  <v-col cols="12" md="8" sm="6">
                    <v-text-field
                      v-model="editedItem.examinationTime"
                      label="Examination time"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" md="8" sm="6">
                    <v-text-field
                      v-model="editedItem.illnessGuid"
                      label="Examination for Illness"
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
          {{ExaminationTypeText(item.value) }}
        </td>
      </tr>
    </template>
    <template v-slot:item="{ item }">
      <tr @click="search(item)">
        <td>{{ ExaminationTypeText(item.type)}}</td>
        <td>{{ item.examinationTime }}</td>
        <td>
          <v-icon size="large" @click.stop="search(item)">mdi-eye</v-icon>
        </td>
      </tr>
    </template>
  </v-data-table-virtual>
</template>

<style scoped>

</style>
