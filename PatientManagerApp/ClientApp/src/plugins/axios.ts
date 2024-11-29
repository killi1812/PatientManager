// src/plugins/axios.ts
import axios from 'axios'

const axiosInstance = axios.create({
  baseURL: '/api',
  timeout: 10000,
})

axiosInstance.interceptors.request.use(
  config => {
    // Ensure no infinite loop in request interceptor
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
