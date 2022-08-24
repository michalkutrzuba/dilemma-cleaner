<template>
  <div class="app">
    <NuxtLayout>
    </NuxtLayout>
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

<style lang="scss">
@import 'assets/styles/normalize.scss';
@import 'assets/styles/main.scss';
</style>