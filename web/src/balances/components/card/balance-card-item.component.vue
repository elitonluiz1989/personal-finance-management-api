<script setup lang="ts">
import { computed, defineProps } from "vue";

type BalanceCardItemValueHandler<T, R> = (value: T) => R;
type BalanceCardItemProps = {
  label: string;
  value: boolean | string | number | Date | object;
  valueHandler?: BalanceCardItemValueHandler<any, any>;
};

const props = defineProps<BalanceCardItemProps>();

const computedValue = computed((): string => {
  if (props.valueHandler instanceof Function)
    return props.valueHandler(props.value);

  return props.value?.toLocaleString();
});
</script>

<template>
  <div class="d-flex row py-3">
    <div class="col-5 fw-bolder">{{ props.label }}</div>
    <div class="col">{{ computedValue }}</div>
  </div>
</template>
