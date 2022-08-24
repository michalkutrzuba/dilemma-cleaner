<template>
  <div class="header">
    <img class="header__image" src="/images/Logo.png" alt="Dilemma Cleaner" />
  </div>
</template>

<style lang="scss" scoped>
@import 'assets/styles/_colors.scss';

.header {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  margin: 1rem 0 2rem;

  &:after {
    content: '';
    width: 100%;
    height: 10px;
    border-radius: 10px;
    background-color: $color-primary;
    margin-top: 1rem;
  }

  &__image {
    height: 120px;
  }
}
</style>
