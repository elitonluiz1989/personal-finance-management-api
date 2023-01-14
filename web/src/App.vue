<script setup lang="ts">
import { Env } from "@/app/helpers/env";
import AppHeader from "@/app/components/layout/header/app-header.component.vue";

const appTitle = Env.get("VUE_APP_TITLE");
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
