/**
 * router/index.ts
 *
 * Automatic routes for `./src/pages/*.vue`
 */

// Composables
import {createRouter, createWebHistory} from 'vue-router/auto'
import {setupLayouts} from 'virtual:generated-layouts'
import {useAuthStore} from "@/stores/auth";
import Index from '@/pages/index.vue';
import Home from '@/pages/landingPage.vue';
import Login from '@/pages/login.vue';
import Register from '@/pages/register.vue';
import PatientDetails from '@/pages/patientDetails.vue';
import IllnessDetails from '@/pages/IllnessDetails.vue';
import ExaminationDetails from '@/pages/ExaminationDetails.vue';
import RegisterPatient from '@/pages/RegisterPatient.vue';
import YourPatients from '@/pages/YourPatients.vue';

const routes = [
  {
    path: '/',
    name: 'Index',
    component: Index,
  },
  {
    path: '/home',
    name: 'Home',
    component: Home,
    meta: {requiresAuth: true,}
  },
  {
    path: '/login',
    name: 'Login',
    component: Login,
  },
  {
    path: '/register',
    name: 'Register',
    component: Register,
  },
  {
    path: '/patient-details/:guid',
    name: 'PatientDetails',
    component: PatientDetails,
    meta: {requiresAuth: true,}
  },
  {
    path: '/illness-details/:guid',
    name: 'IllnessDetails',
    component: IllnessDetails,
    meta: {requiresAuth: true,}
  },
  {
    path: '/examination-details/:guid',
    name: 'ExaminationDetails',
    component: ExaminationDetails,
    meta: {requiresAuth: true,}
  },
  {
    path: '/register-patient',
    name: 'RegisterPatient',
    component: RegisterPatient,
    meta: {requiresAuth: true,}
  },
  {
    path: '/your-patients',
    name: 'YourPatients',
    component: YourPatients,
    meta: {requiresAuth: true,}
  },
];

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
