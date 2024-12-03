// src/stores/auth.ts
import {defineStore} from 'pinia'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    isLoggedIn: false,
    //TODO check how to make it undefinded
    username: "",
    token: "",
  }),
  actions: {
    logout() {
      this.isLoggedIn = false
      this.token = ""
      this.username = ""
    },
    setUsername(username: string) {
      this.username = username
    },
    getToken() {
      if (this.token == "") return undefined;
      return this.token
    },
    setToken(token: string) {
      this.isLoggedIn = true
      this.token = token
    }
  }
})
