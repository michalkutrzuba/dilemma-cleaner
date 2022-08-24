<template>
  <div class="footer">
    <div class="footer__content">
      <div class="footer__text">
        {{ footerText }}
      </div>
      <div class="footer__socials">
        <a
          class="footer__social-button footer__social-button--mail"
          :href="emailButton.url"
          :title="emailButton.text"
        ></a>
        <a
          class="footer__social-button footer__social-button--linkedin"
          target="_blank"
          :href="linkedInButton.url"
          :title="linkedInButton.text"
        ></a>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { useConfigurationStore } from '~/store/configuration'

interface SocialButton {
  text: string
  url: string
}

const configurationStore = useConfigurationStore()

const emailButton = computed<SocialButton>(() => ({
  text: configurationStore.translations.shared.author.sendEmail,
  url: `mailto:${configurationStore.settings.authorEmail}`,
}))

const linkedInButton = computed<SocialButton>(() => ({
  text: configurationStore.translations.shared.author.openLinkedIn,
  url: configurationStore.settings.authorLinkedIn,
}))

const footerText = computed<string>(() => configurationStore.translations.shared.footerText)
</script>

<style lang="scss" scoped>
@import 'assets/styles/_colors.scss';

.footer {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  margin-top: 2rem;
  padding-bottom: 1rem;

  &:before {
    content: '';
    width: 100%;
    height: 10px;
    border-radius: 10px;
    background-color: $color-primary;
    margin-bottom: 1rem;
  }

  &__content {
    display: flex;
    justify-content: space-around;
    align-items: center;
    width: 100%;
  }

  &__socials {
    display: flex;
    gap: 10px;
  }

  &__social-button {
    display: block;
    width: 50px;
    height: 50px;
    background-color: $color-primary;
    border-radius: 50px;
    background-position: center;
    background-repeat: no-repeat;

    &--mail {
      background-image: url('/icons/mail.svg');
      background-size: 55%;
    }

    &--linkedin {
      background-image: url('/icons/linkedin.svg');
      background-size: 45%;
    }

    &:hover {
      background-color: $color-secondary;
    }
  }
}
</style>