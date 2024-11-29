<template>
  <v-app>
    <Navbar v-if="authstore.isLoggedIn"/>
    <v-main>
      <router-view/>
    </v-main>

    <AppFooter/>
  </v-app>
</template>

<script lang="ts" setup>
//
import Navbar from "@/components/Navbar.vue";
import {useAuthStore} from "@/stores/auth";
const authstore= useAuthStore()
</script>
