<template>
  <div class="dilemma">
    <AppLoader v-if="isLoading" />
    <template v-else>
      <a class="dilemma__back" @click="onBack">{{ translations.backButton }}</a>
      <div class="dilemma__header">
        <div class="dilemma__image"><img :src="model.imageUrl" :alt="model.imageAlt" /></div>
        <div class="dilemma__title">{{ model.title }}</div>
      </div>
      <div class="dilemma__description" v-html="model.descriptionHtml"></div>
      <div class="dilemma__buttons">
        <a class="dilemma__start" @click="onStart">{{ translations.startButton }}</a>
      </div>
    </template>
  </div>
</template>

<script lang="ts" setup>
import { DilemmaModel, useDilemmasStore } from '~/store/dilemmas'
import { TranslationsModelDilemmaPage, useConfigurationStore } from '~/store/configuration'
import { useUserSelectionsStore } from '~/store/userSelections'

const configurationStore = useConfigurationStore()
const translations = computed<TranslationsModelDilemmaPage>(() => configurationStore.translations.dilemma.page)

const userSelectionsStore = useUserSelectionsStore()
const router = useRouter()
const route = useRoute()
const uid = computed<string>(() => route.params.uid as string)

const dilemmasStore = useDilemmasStore()
const isLoading = computed<boolean>(() => dilemmasStore.current?.uid !== uid.value)
const model = computed<DilemmaModel>(() => dilemmasStore.current!)

onMounted(async () => {
  if (isLoading) {
    dilemmasStore.current = null
    userSelectionsStore.clear()
    await dilemmasStore.fetchByUid(uid.value)
  }
})

function onBack() {
  router.push({ path: `/dilemmas` })
}

function onStart() {
  userSelectionsStore.push(model.value.firstStepId)
  router.push({ path: `/dilemmas/${uid.value}/${model.value.firstStepId}` })
}
</script>

<style lang="scss" scoped>
@import 'assets/styles/_colors.scss';

$size: 160px;

.dilemma {
  display: flex;
  flex-direction: column;
  gap: 30px;

  &__header {
    display: flex;
    align-items: center;
    gap: 20px;
    justify-content: center;
  }

  &__title {
    color: $color-action;
    text-align: center;
    font-size: 3rem;
    font-weight: 900;
  }

  &__image {
    width: $size;
    height: $size;
    border-radius: $size;
    outline: 6px solid $color-action;
    overflow: hidden;

    img {
      max-width: $size;
      max-height: $size;
    }
  }

  &__back {
    display: flex;
    align-items: center;
    cursor: pointer;

    &:before {
      content: '‹';
      font-size: 3rem;
      margin-top: -7px;
    }

    &:hover {
      font-weight: bold;
    }
  }

  &__buttons {
    display: flex;
    align-items: center;
    justify-content: center;
  }

  &__start {
    background-color: $color-action;
    padding: 20px 40px;
    border-radius: 10px;
    font-weight: bold;
    cursor: pointer;

    &:hover {
      font-weight: 900;
      background-color: $color-accent;
    }
  }
}
</style>