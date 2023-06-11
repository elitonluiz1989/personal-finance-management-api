<script setup lang="ts">
import { BootstrapColorsEnum } from "@/app/enums/BootstrapColorsEnum.enum";
import { CurrencyFormatterStatic } from "@/app/helpers/currency-formatter";
import { Installment } from "@/installments/installment.model";
import { computed, defineEmits, defineProps, ref, withDefaults } from "vue";
import AppBadge from "@/app/components/bagde/app-badge.component.vue";
import InstallmentBadge from "@/installments/components/installment-badge.component.vue";

type TransactionsFormInstallmentPropsType = {
  installment: Installment;
  allowSelection: boolean;
};
type TransactionsFormInstallmentEmitsType = {
  (e: "onSelectInstallment", installment: Installment): void;
};

const selected = ref<boolean>(false);

const props = withDefaults(
  defineProps<TransactionsFormInstallmentPropsType>(),
  {
    allowSelection: true,
  }
);
const emits = defineEmits<TransactionsFormInstallmentEmitsType>();

const installmentNumberDescription = `${props.installment.number}/${props.installment.balance?.installmentsNumber}`;

const cssStyles = computed(() => ({
  "btn btn-outline-secondary d-flex flex-row justify-content-between p-2 w-100 selected":
    selected.value,
  "btn btn-outline-secondary d-flex flex-row justify-content-between p-2 w-100":
    selected.value === false,
}));

const select = () => {
  if (selected.value) {
    selected.value = false;

    emits("onSelectInstallment", props.installment);

    return;
  }

  if (props.allowSelection === false) return;

  selected.value = true;

  emits("onSelectInstallment", props.installment);
};
</script>

<template>
  <button :class="cssStyles" @click.prevent="select()">
    <div>
      <span class="fw-bolder me-2">
        {{ props.installment.balance?.name }}
      </span>

      <AppBadge
        :color="BootstrapColorsEnum.primary"
        :text="installmentNumberDescription"
      />

      <span class="ms-2">
        {{ CurrencyFormatterStatic.format(installment.amount) }}
      </span>
    </div>

    <InstallmentBadge
      :status="installment.status"
      :description="installment.statusDescription"
    />
  </button>
</template>

<style scoped lang="scss">
@import "bootstrap/scss/functions";
@import "bootstrap/scss/variables";

.selected {
  background-color: $secondary;
  color: white;

  &:hover {
    background-color: $secondary;
    color: white;
  }
}
</style>
