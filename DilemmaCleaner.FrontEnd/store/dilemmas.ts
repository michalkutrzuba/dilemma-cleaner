import { defineStore } from 'pinia'
import { ApiClient, isApiError } from '~/api/ApiClient'

interface State {
  list: DilemmasListModel | null
  current: DilemmaModel | null
}

export const useDilemmasStore = defineStore('dilemmas', {
  state: (): State => ({
    list: null,
    current: null,
  }),
  getters: {
    hasList: state => !!state?.list?.items && state.list.items.length > 0,
  },
  actions: {
    async fetchAll() {
      const api = new ApiClient()
      const data = await api.get<DilemmasListModel>('dilemmas')

      if (isApiError(data)) {
        return
      }

      this.list = data
    },
    async fetchByUid(uid: string) {
      const api = new ApiClient()
      const data = await api.get<DilemmaModel>(`dilemmas/${uid}`)

      if (isApiError(data)) {
        return
      }

      this.current = data
    },
  },
})

export interface DilemmaModel {
  uid: string
  title: string
  imageUrl: string
  imageAlt: string
  descriptionHtml: string
  firstStepId: string
  steps: DilemmaModelStep[]
}

export interface DilemmaModelStep {
  id: string
  title: string
  descriptionHtml: string
  choices: DilemmaModelStepChoice[]
}

export interface DilemmaModelStepChoice {
  text: string
  nextStepId: string
}

export interface DilemmasListModel {
  items: DilemmasListModelItem[]
}

export interface DilemmasListModelItem {
  uid: string
  title: string
  imageUrl: string
  imageAlt: string
}
