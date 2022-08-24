<template>
  <div class="results">
    <div class="results__header">
      <a class="results__back" @click="onBackToList">{{ translations.dilemma.result.backButton }}</a>
      <div class="results__title">Your History</div>
      <a class="results__again" @click="onStartAgain">{{ translations.dilemma.result.startAgainButton }}</a>
    </div>
    <div class="results__activity">
      <div class="results__activity-item" v-for="(step, index) in selectedSteps" :key="step.id">
        <div class="results__activity-item-number">{{ index + 1 }}</div>
        <div class="results__activity-item-title">{{ step.title }}</div>
        <div class="results__activity-item-choice" v-if="step.choice">{{ step.choice }}</div>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { useDilemmasStore } from '~/store/dilemmas'
import { useUserSelectionsStore } from '~/store/userSelections'
import { TranslationsModel, useConfigurationStore } from '~/store/configuration'

interface SelectedStep {
  id: string
  title: string
  choice: string | null
}

const configurationStore = useConfigurationStore()
const translations = computed<TranslationsModel>(() => configurationStore.translations)

const userSelections = useUserSelectionsStore()
const dilemmasStore = useDilemmasStore()

const selectedSteps = computed<SelectedStep[]>(() => {
  if (!dilemmasStore.current) return []

  const steps = dilemmasStore.current.steps

  return userSelections.stepIds.map((id, index, stepIds) => {
    const currentStep = steps.find(step => step.id === id)!
    const nextStepId = stepIds[index + 1]
    const selectedChoice = !!nextStepId ? currentStep.choices.find(choice => choice.nextStepId === nextStepId) : null

    return {
      id: id,
      title: currentStep.title,
      choice: selectedChoice?.text || null,
    }
  })
})

const route = useRoute()
const router = useRouter()

function onBackToList() {
  router.push('/dilemmas')
}

function onStartAgain() {
  userSelections.clear()
  router.push({ path: `/dilemmas/${route.params.uid}` })
}
</script>

<style lang="scss" scoped>
@import 'assets/styles/_colors.scss';

.results {
  &__header {
    display: flex;
    align-items: center;
    justify-content: space-between;
  }

  &__title {
    flex-grow: 2;
    color: $color-action;
    text-align: center;
    font-size: 2rem;
    font-weight: 900;
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

  &__again {
    display: block;
    background-color: $color-primary;
    padding: 10px 20px;
    border-radius: 10px;
    font-weight: bold;
    cursor: pointer;

    &:hover {
      font-weight: 900;
      background-color: $color-secondary;
    }
  }

  &__activity {
    display: flex;
    flex-direction: column;
    margin: 20px;

    // this should be another component
    &-item {
      display: flex;
      align-items: center;
      gap: 20px;
      padding: 10px 20px;
      font-size: 1.125rem;

      & + & {
        border-top: 2px solid $color-accent;
      }

      &-number {
        font-weight: 900;
        color: $color-primary;

        &:before {
          content: '#';
        }
      }

      &-title {
      }

      &-choice {
        display: flex;
        align-items: center;
        gap: 10px;

        &:before {
          content: '👉';
          font-size: 2rem;
        }
      }
    }
  }
}
</style>