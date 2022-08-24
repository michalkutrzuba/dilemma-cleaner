<template>
  <div class="dilemmas">
    <div class="dilemmas__title">{{ title }}</div>
    <div class="dilemmas__description" v-html="description"></div>
    <DilemmasList class="dilemmas__list" />
  </div>
</template>

<script setup lang="ts">
definePageMeta({
  alias: '/',
})

import { useConfigurationStore } from '~/store/configuration'

const configurationStore = useConfigurationStore()

const title = computed<string>(() => configurationStore.translations.list.title)
const description = computed<string>(() => configurationStore.translations.list.description)
</script>

<style lang="scss" scoped>
@import 'assets/styles/_colors.scss';

.dilemmas {
  &__title {
    color: $color-action;
    text-align: center;
    font-size: 3rem;
    font-weight: 900;
  }
}
</style>