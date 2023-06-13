<script setup lang="ts">
import { isNullOrUndefined, isNullOrWhiteSpace } from "@/app/helpers/helpers";
import { CurrencyFormatter } from "@/app/helpers/currency-formatter";
import { defineEmits, defineProps, ref, watch, withDefaults } from "vue";

type AppFormInputProps = {
  class: string;
  formField: string;
  modelValue: number;
  maxlength?: number;
  location?: string;
  currency?: string;
  disabled: boolean;
};
type AppFormInputEmits = {
  (e: "update:modelValue", show: number): void;
  (e: "onFocus", evt: Event): void;
  (e: "onKeyUp", evt: Event): void;
};
const props = withDefaults(defineProps<AppFormInputProps>(), {
  location: navigator?.language,
  maxlength: 200,
  disabled: false,
});
const emits = defineEmits<AppFormInputEmits>();
const rawValue = ref<number>();
const currencyFormatter = new CurrencyFormatter(props.location, props.currency);

const watchValueChange = (newValue: number, oldValue: number): void => {
  if (newValue === oldValue) return;

  const elements = document.querySelectorAll("input[type=text]");

  if (!elements || elements?.length === 0) return;

  for (const element of elements) {
    element.setAttribute("value", newValue.toString());
  }
};
const currencyHandler = (evt: Event): void => {
  const { target } = evt;
  const element = target as HTMLInputElement;
  let value = element?.value.toString().replace(/[^\d]/g, "");

  if (isNullOrWhiteSpace(value)) return;

  if (value.length < 3) value = value.padStart(3, "0");

  const valueLength = value.length;
  value =
    value.substring(0, valueLength - 2) +
    "." +
    value.substring(valueLength - 2);

  rawValue.value = Number.parseFloat(value);

  if (isNaN(rawValue.value)) rawValue.value = 0;

  element.value = currencyFormatter.format(rawValue.value);

  emits("update:modelValue", rawValue.value);
};
const triggerFocus = (evt: Event): void => {
  emits("onFocus", evt);
};
const onKeyUpHandler = (evt: Event): void => {
  currencyHandler(evt);

  emits("onKeyUp", evt);
};

watch(() => props.modelValue, watchValueChange);
</script>
<template>
  <div class="position-relative w-100">
    <input
      type="text"
      :class="props.class"
      :maxlength="props.maxlength"
      :form-field="props.formField"
      :disabled="props.disabled"
      @focus="triggerFocus"
      @keyup="onKeyUpHandler"
      v-if="isNullOrUndefined(props.maxlength)"
    />

    <input
      type="text"
      :class="props.class"
      :form-field="props.formField"
      :disabled="props.disabled"
      @focus="triggerFocus"
      @keyup="onKeyUpHandler"
      v-else
    />
  </div>
</template>
