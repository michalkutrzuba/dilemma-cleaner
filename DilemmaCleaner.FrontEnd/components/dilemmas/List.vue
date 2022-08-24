<template>
  <div class="list">
    <div class="list__header">{{ title }}</div>
    <div class="list__items">
      <AppLoader v-if="isLoading" />
      <template v-else>
        <DilemmasListItem class="list__item" v-for="item in items" :key="item.uid" :model="item" />
      </template>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { useConfigurationStore } from '~/store/configuration'
import { DilemmasListModelItem, useDilemmasStore } from '~/store/dilemmas'

const configurationStore = useConfigurationStore()
const title = computed<string>(() => configurationStore.translations.list.dilemmasTitle)

const dilemmasStore = useDilemmasStore()
const isLoading = computed<boolean>(() => !dilemmasStore.hasList)
const items = computed<DilemmasListModelItem[]>(() => dilemmasStore.list!.items)

onMounted(() => (dilemmasStore.current = null))
</script>

<style lang="scss" scoped>
@import 'assets/styles/_colors.scss';

.list {
  margin: 20px 40px;

  &__header {
    color: $color-action;
    font-size: 1.5rem;
    font-weight: 700;
  }

  &__items {
    margin-top: 30px;
    margin-left: 10px;
  }

  &__item {
    & + & {
      margin-top: 20px;
    }
  }
}
</style>