<script setup lang="ts">
import { defineProps } from "vue";
import { ValidationRule } from "../../types";

type AppValidationMessagesProps = {
  validations: ValidationRule<any>[];
};

const props = defineProps<AppValidationMessagesProps>();
</script>

<template>
  <small
    class="d-inline-block w-100 text-danger"
    v-for="(validation, index) of props.validations"
    :key="index"
  >
    {{ validation.message }}
  </small>
</template>
