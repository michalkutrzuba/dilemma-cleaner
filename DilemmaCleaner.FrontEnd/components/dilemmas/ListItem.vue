<template>
  <div class="item" @click="onClick">
    <div class="item__image"><img :src="model.imageUrl" :alt="model.imageAlt" /></div>
    <div class="item__header">{{ model.title }}</div>
  </div>
</template>

<script lang="ts" setup>
import { DilemmasListModelItem } from '~/store/dilemmas'

interface Props {
  model: DilemmasListModelItem
}

const props = defineProps<Props>()

const router = useRouter()

function onClick() {
  router.push({ path: `/dilemmas/${props.model.uid}` })
}
</script>

<style lang="scss" scoped>
@import 'assets/styles/_colors.scss';

$size: 60px;

.item {
  display: flex;
  align-items: center;
  gap: 20px;
  cursor: pointer;

  &__image {
    width: $size;
    height: $size;
    border-radius: $size;
    outline: 6px solid $color-accent;
    overflow: hidden;

    img {
      max-width: $size;
      max-height: $size;
    }
  }

  &__header {
    flex-grow: 2;
  }

  &:hover {
    .item__image {
      outline-color: $color-action;
      transform: scale(110%);
    }

    .item__header {
      font-weight: bold;
    }
  }
}
</style>