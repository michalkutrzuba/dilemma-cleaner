<template>
  <div class="step">
    <div class="step__title">{{ model.title }}</div>
    <div v-if="hasDescription" class="step__description" v-html="model.descriptionHtml"></div>

    <template v-if="!isFinalStep">
      <div class="step__choices">
        <div class="step__choice" v-for="choice in model.choices" :key="choice.id">
          <a @click="() => goToNext(choice.nextStepId)">{{ choice.text }}</a>
        </div>
      </div>
    </template>
  </div>
</template>

<script lang="ts" setup>
import { DilemmaModelStep, useDilemmasStore } from '~/store/dilemmas'
import { useUserSelectionsStore } from '~/store/userSelections'

const dilemmasStore = useDilemmasStore()
const userSelections = useUserSelectionsStore()

const router = useRouter()
const route = useRoute()
const id = computed<string>(() => route.params.stepId as string)
const model = computed<DilemmaModelStep>(() => dilemmasStore.current!.steps.find(step => step.id == id.value)!)
const isFinalStep = computed<boolean>(() => model.value.choices.length === 0)
const hasDescription = computed<boolean>(() => model.value.descriptionHtml.length > 0)

function goToNext(nextStepId: string) {
  userSelections.push(nextStepId)
  router.push({ path: `/dilemmas/${route.params.uid as string}/${nextStepId}` })
}

function goToResults() {
  router.push({ path: `/dilemmas/${route.params.uid as string}/results` })
}
</script>

<style lang="scss" scoped>
@import 'assets/styles/_colors.scss';

.step {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  height: 100%;

  &__title {
    color: $color-action;
    text-align: center;
    font-size: 2rem;
    font-weight: bold;
  }

  &__choices {
    flex-grow: 2;
    display: flex;
    align-items: center;
    justify-content: center;
    gap: 30px;
  }

  &__choice {
    a {
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
}
</style>