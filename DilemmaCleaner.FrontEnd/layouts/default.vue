<template>
  <div class="layout">
  </div>
</template>

<script lang="ts" setup>
import { useDilemmasStore } from '~/store/dilemmas'

const dilemmasStore = useDilemmasStore()

onMounted(async () => {
  if (!dilemmasStore.hasList) {
    await dilemmasStore.fetchAll()
  }
})
</script>
