<script setup lang="ts">
import { getEventTarget, isValidNumber } from "@/app/helpers/helpers";
import { defineEmits, defineProps, ref, watch, withDefaults } from "vue";

type AppFormInputNumberProps = {
  class: string;
  formField: string;
  modelValue: number;
  min?: number;
  max?: number;
  disabled: boolean;
  onFocus: (e: Event) => void;
};
type AppFormInputNumberEmits = {
  (e: "update:modelValue", value: number): void;
};

const props = withDefaults(defineProps<AppFormInputNumberProps>(), {
  disabled: false,
});
const emits = defineEmits<AppFormInputNumberEmits>();
const min = Number(props.min);
const max = Number(props.max);
const value = ref<number>();

watch(
  () => props.modelValue,
  (value: number) => {
    const elements = document.querySelectorAll<HTMLInputElement>(
      'input.form-control[type="number"]'
    );

    if (elements) {
      elements.forEach((e) => (e.value = value.toString()));
    }
  }
);

const onFocus = (evt: Event) => props.onFocus(evt);
const onkeyup = (evt: Event) => {
  if (isNaN(min) && isNaN(max)) return;

  const target = getEventTarget<HTMLInputElement>(evt);
  const inputValue = target.value?.toString().replace(/[^\d]/g, "");
  value.value = Number(inputValue);

  if (isNaN(value.value)) value.value = 0;

  if (isNaN(min) === false && value.value < min) value.value = 0;

  if (isNaN(max) === false && value.value > max) {
    const strValue = value.value.toString();
    value.value = Number(strValue.substring(0, max.toString().length));
    target.value = value.value.toString();

    return;
  }

  if (value.value <= 0) target.value = "";

  emits("update:modelValue", value.value);
};
</script>

<template>
  <input
    type="number"
    class="form-control"
    form-field="installmentsNumber"
    :min="min"
    :disabled="props.disabled"
    @focus="onFocus"
    @keyup="onkeyup"
    v-if="isValidNumber(min) && isValidNumber(max) === false"
  />

  <input
    type="number"
    class="form-control"
    form-field="installmentsNumber"
    :max="max"
    :disabled="props.disabled"
    @focus="onFocus"
    @keyup="onkeyup"
    v-if="isValidNumber(min) === false && isValidNumber(max)"
  />

  <input
    type="number"
    class="form-control"
    form-field="installmentsNumber"
    :min="min"
    :max="max"
    :disabled="props.disabled"
    @focus="onFocus"
    @keyup="onkeyup"
    v-if="isValidNumber(min) && isValidNumber(max)"
  />
</template>
