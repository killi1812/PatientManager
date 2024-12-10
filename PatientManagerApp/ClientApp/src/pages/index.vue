<template>
</template>

<script lang="ts" setup>
const router = useRouter()
const home =async () => {
  await router.replace({name: "Home"})
}
home()
</script>
