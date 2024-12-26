/**
 * plugins/index.ts
 *
 * Automatically included in `./src/main.ts`
 */

// Plugins
import vuetify from './vuetify'
import pinia from '../stores'
import router from '../router'

// Types
import type { App } from 'vue'
import axiosInstance from "@/plugins/axios";

export function registerPlugins (app: App) {
  app
    .use(axiosInstance)
    .use(vuetify)
    .use(router)
    .use(pinia)
}
