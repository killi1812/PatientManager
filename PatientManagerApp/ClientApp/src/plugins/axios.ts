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
    if (token) {
      config.headers.Authorization = `Bearer ${token}`
    }
      return config
  },
  error => {
    return Promise.reject(error)
  }
)

axiosInstance.interceptors.response.use(
  response => {
    // Ensure no infinite loop in response interceptor
    return response
  },
  error => {
    return Promise.reject(error)
  }
)

export default axiosInstance
