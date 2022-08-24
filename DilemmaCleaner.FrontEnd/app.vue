<template>
  <div class="app">
    <NuxtLayout v-if="!isLoading">
      <NuxtPage />
    </NuxtLayout>
    <AppFullPageLoader v-else />
  </div>
</template>

<script setup lang="ts">
import { useConfigurationStore } from '~/store/configuration'

const configurationStore = useConfigurationStore()

const isLoading = computed<boolean>(() => !configurationStore.configuration)

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