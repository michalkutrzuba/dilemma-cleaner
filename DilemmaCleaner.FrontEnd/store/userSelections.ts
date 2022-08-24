import { defineStore } from 'pinia'

interface State {
  stepIds: string[]
}

export const useUserSelectionsStore = defineStore('user-selections', {
  state: (): State => ({
    stepIds: [],
  }),
  actions: {
    clear() {
      this.stepIds = []
    },
    push(id: string) {
      this.stepIds.push(id)
    },
  },
})
