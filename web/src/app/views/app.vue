<script setup lang="ts">
import AppHeader from "@/app/components/layout/header/app-header.component.vue";
import { vueEnv } from "@/app/helpers/helpers";

const appTitle = vueEnv("TITLE");
</script>

<template>
  <AppHeader :title="appTitle" />

  <div class="layout-session container-fluid">
    <div class="row">
      <div class="col-12">
        <router-view />
      </div>
    </div>
  </div>
</template>
