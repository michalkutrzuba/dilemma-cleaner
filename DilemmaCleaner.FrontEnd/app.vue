<template>
  <div class="app">
  </div>
</template>

<script setup lang="ts">
import { useConfigurationStore } from '~/store/configuration'

const configurationStore = useConfigurationStore()

onMounted(async () => {
  if (!configurationStore.configuration) {
    await configurationStore.fetch()
  }
})
</script>
