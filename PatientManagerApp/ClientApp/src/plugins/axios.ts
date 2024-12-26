// src/plugins/axios.ts
import axios from 'axios'
import {useAuthStore} from "@/stores/auth";

const axiosInstance = axios.create({
  baseURL: '/api',
  timeout: 10000,
})

axiosInstance.interceptors.request.use(
  config => {
    const authStore = useAuthStore()
    const token = authStore.getToken()
    console.log(token)
    if (token) {
      config.headers.Authorization = `Bearer ${token}`
    }
      return config
  },
  error => {
    return Promise.reject(error)
  }
)

export default axiosInstance
