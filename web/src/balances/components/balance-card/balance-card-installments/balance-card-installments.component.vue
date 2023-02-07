<script setup lang="ts">
import { computed, defineProps } from "vue";
import { Installment } from "@/installments/installment.model";
import { BalanceTypeEnum } from "@/balances/balances-type.enum";
import AppAccordion from "@/app/components/accordion/app-accordion.component.vue";

type BalanceCardInstallmentsProp = {
  installments: Installment[];
  type: BalanceTypeEnum;
};

const props = defineProps<BalanceCardInstallmentsProp>();

const cssStyles = computed(() => ({
  "balance__installments--credit": props.type === BalanceTypeEnum.credit,
  "balance__installments--debt": props.type === BalanceTypeEnum.debt,
}));
</script>

<template>
  <AppAccordion
    title="Installments"
    class="balance__installments"
    :class="cssStyles"
    v-if="props.installments?.length > 1"
  >
    <ul class="balance__installments-list">
      <li v-for="(installment, index) in props.installments" :key="index">
        <span>{{ installment.number }}</span>

        <span>{{ installment.reference }}</span>

        <span>{{ installment.status }}</span>
      </li>
    </ul>
  </AppAccordion>
</template>

<style scoped lang="scss">
@import "../balance-card.scss";

.balance__installments {
  $selector: &;

  &--credit {
    border-top: 1px solid $card-credit-color;

    #{$selector}-list {
      border-top: 1px solid $card-credit-color;
    }
  }

  &--debt {
    border-top: 1px solid $card-debt-color;

    #{$selector}-list {
      border-top: 1px solid $card-debt-color;
    }
  }
}
</style>
