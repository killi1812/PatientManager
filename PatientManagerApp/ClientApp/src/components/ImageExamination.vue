<script lang="ts" setup>

import {getExaminationFile} from "@/api/ExaminationApi";

const props = defineProps({
  imgName: {
    type: String,
    required: true
  }
})

const imageData = ref();
const loading = ref(false);

const getImage = async () => {
  loading.value = true;
  try {
    imageData.value = await getExaminationFile(props.imgName)
  } finally {
    loading.value = false;
  }
}

onMounted(async () => {
  await getImage()
})
</script>

<template>
  <div v-if="loading">
    Loading...
  </div>
  <div v-else class="container">
    <img :src="imageData" alt="No picture" >
  </div>
</template>

<style scoped>
.container {
 width: 50rem;
}
</style>
