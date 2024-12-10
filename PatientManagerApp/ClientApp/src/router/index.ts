/**
 * router/index.ts
 *
 * Automatic routes for `./src/pages/*.vue`
 */

// Composables
import { createRouter, createWebHistory } from 'vue-router/auto'
import { setupLayouts } from 'virtual:generated-layouts'
import {useAuthStore} from "@/stores/auth";

const routes = [
  {
    path: '/',
    name: 'Index',
    component: () => import('@/pages/index.vue'),
  },
  {
    path: '/home',
    name: 'Home',
    component: () => import('@/pages/landingPage.vue'),
    meta:{ requiresAuth: true, }
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('@/pages/login.vue'),
  },
  {
    path: '/register',
    name: 'Register',
    component: () => import('@/pages/register.vue'),
  },
  {
    path: '/patient-details:guid',
    name: 'PatientDetails',
    component: () => import('@/pages/patientDetails.vue'),
    meta:{ requiresAuth: true, }
  }

 ]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: setupLayouts(routes),
})

router.beforeEach((to, from, next) => {
  const authStore = useAuthStore()
  if (to.meta.requiresAuth && !authStore.isLoggedIn) {
    next('/login')
  } else {
    next()
  }
})


// Workaround for https://github.com/vitejs/vite/issues/11804
router.onError((err, to) => {
  if (err?.message?.includes?.('Failed to fetch dynamically imported module')) {
    if (!localStorage.getItem('vuetify:dynamic-reload')) {
      console.log('Reloading page to fix dynamic import error')
      localStorage.setItem('vuetify:dynamic-reload', 'true')
      location.assign(to.fullPath)
    } else {
      console.error('Dynamic import error, reloading page did not fix it', err)
    }
  } else {
    console.error(err)
  }
})

router.isReady().then(() => {
  localStorage.removeItem('vuetify:dynamic-reload')
})

export default router
