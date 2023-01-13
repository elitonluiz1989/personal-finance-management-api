<script setup lang="ts">
import AppHeader from "@/components/layout/header/AppHeader.vue";
import { Env } from "./helpers/Env";

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
