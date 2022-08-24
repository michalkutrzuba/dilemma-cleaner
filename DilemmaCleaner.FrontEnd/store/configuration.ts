import { defineStore } from 'pinia'
import { ApiClient, isApiError } from '~/api/ApiClient'

interface State {
  configuration: ConfigurationModel | null
}

export const useConfigurationStore = defineStore('configuration', {
  state: (): State => ({
    configuration: null,
  }),
  getters: {
    settings: (state): SettingsModel => state.configuration!.settings,
    translations: (state): TranslationsModel => state.configuration!.translations,
  },
  actions: {
    async fetch() {
      const api = new ApiClient()
      const data = await api.get<ConfigurationModel>('config')

      if (isApiError(data)) {
        return
      }

      this.configuration = data
    },
  },
})

export interface ConfigurationModel {
  settings: SettingsModel
  translations: TranslationsModel
}

export interface SettingsModel {
  authorEmail: string
  authorLinkedIn: string
}

export interface TranslationsModel {
  shared: TranslationsModelShared
  list: TranslationsModelList
  dilemma: TranslationsModelDilemma
}

export interface TranslationsModelShared {
  author: TranslationsModelSharedAuthor
  footerText: string
}

export interface TranslationsModelSharedAuthor {
  sendEmail: string
  openLinkedIn: string
}

export interface TranslationsModelList {
  title: string
  description: string
  dilemmasTitle: string
}

export interface TranslationsModelDilemma {
  page: TranslationsModelDilemmaPage
  step: TranslationsModelDilemmaStep
  result: TranslationsModelDilemmaResult
}

export interface TranslationsModelDilemmaPage {
  backButton: string
  startButton: string
}

export interface TranslationsModelDilemmaStep {
  finishEarlierDividerLabel: string
  finishEarlierButton: string
}

export interface TranslationsModelDilemmaResult {
  backButton: string
  startAgainButton: string
  showFlowchartButton: string
  hideFlowchartButton: string
}
