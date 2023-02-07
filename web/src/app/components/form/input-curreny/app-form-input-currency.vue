<script setup lang="ts">
import { isNullOrUndefined, isNullOrWhiteSpace } from "@/app/helpers/helpers";
import { CurrencyFormatter } from "@/app/helpers/currency-formatter";
import { defineEmits, defineProps, ref, withDefaults } from "vue";

type AppFormInputProps = {
  class: string;
  formField: string;
  modelValue: number;
  maxlength?: number;
  location?: string;
  currency?: string;
  onFocus: (e: Event) => void;
};
type AppFormInputEmits = {
  (e: "update:modelValue", show: number): void;
};
const props = withDefaults(defineProps<AppFormInputProps>(), {
  location: navigator?.language,
  maxlength: 200,
});
const emits = defineEmits<AppFormInputEmits>();
const rawValue = ref<number>();
const currencyFormatter = new CurrencyFormatter(props.location, props.currency);

const currencyHandler = (evt: Event) => {
  const { target } = evt;
  const element = target as HTMLInputElement;
  let value = element?.value.toString().replace(/[^\d]/g, "");

  if (isNullOrWhiteSpace(value)) return "";

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
const triggerFocus = (evt: Event) => props.onFocus(evt);
</script>

<template>
  <div class="position-relative w-100">
    <input
      type="text"
      :class="props.class"
      :maxlength="props.maxlength"
      :form-field="props.formField"
      @focus="triggerFocus"
      @keyup="currencyHandler"
      v-if="isNullOrUndefined(props.maxlength)"
    />

    <input
      type="text"
      :class="props.class"
      :maxlength="props.maxlength"
      :form-field="props.formField"
      @focus="triggerFocus"
      @keyup="currencyHandler"
    />
  </div>
</template>
