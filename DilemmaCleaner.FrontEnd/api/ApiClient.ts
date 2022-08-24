export class ApiClient {
  private readonly _baseUrl: string = useAppConfig().api.baseUrl

  async get<T>(request: string): Promise<T | ApiError> {
    try {
      const data = await $fetch(request, {
        baseURL: this._baseUrl,
        headers: { Accept: 'application/json' },
      })
      return data as T
    } catch (error: any) {
      const message = `Get action to ${request} failed.`
      console.error(message, error)

      return { message }
    }
  }
}

export interface ApiError {
  message: string
}

export function isApiError(test: any): test is ApiError {
  return test?.message && typeof test.message === 'string'
}
