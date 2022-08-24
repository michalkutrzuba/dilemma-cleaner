<template>
  <div class="layout">
    <AppHeader />
    <div class="layout__content">
      <slot></slot>
    </div>
    <AppFooter />
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

<style lang="scss" scoped>
.layout {
  display: flex;
  flex-direction: column;
  height: 100%;
  margin: 0 auto;
  max-width: 1024px;

  &__content {
    flex-grow: 2;
  }
}
</style>