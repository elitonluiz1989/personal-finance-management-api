<script setup lang="ts">
import { defineProps, withDefaults } from "vue";

type AppHeaderTitlePropsType = {
  text: string;
  url?: string;
};
const props = withDefaults(defineProps<AppHeaderTitlePropsType>(), {
  url: "#",
});
</script>

<template>
  <a class="navbar-brand" :href="props.url">{{ props.text }}</a>
</template>
