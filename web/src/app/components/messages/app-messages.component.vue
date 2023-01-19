<script setup lang="ts">
import * as AppMessagesTypeEnumModule from "./app-messages-type.enum";
import { defineProps } from "vue";

type AppMessagesType = {
  messages: string[];
  type: AppMessagesTypeEnumModule.AppMessagesTypeEnum;
};
const props = defineProps<AppMessagesType>();

const handleCssClass = () =>
  `alert alert-${AppMessagesTypeEnumModule.getDescription(props.type)}`;
</script>

<template>
  <div :class="handleCssClass()" role="alert">
    <p class="mb-0" v-for="(message, index) in props.messages" :key="index">
      {{ message }}
    </p>
  </div>
</template>

<style scoped>
p + p {
  margin-top: 1rem;
}
</style>
