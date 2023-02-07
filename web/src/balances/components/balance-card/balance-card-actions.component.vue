<script setup lang="ts">
import { BalanceTypeEnum } from "@/balances/balances-type.enum";
import { computed, defineProps } from "vue";

type BalanceActionsProps = {
  id: number;
  type: BalanceTypeEnum;
};

const props = defineProps<BalanceActionsProps>();

const cssStyles = computed(() => ({
  "balance__actions--credit": props.type === BalanceTypeEnum.credit,
  "balance__actions--debt": props.type === BalanceTypeEnum.debt,
}));
</script>

<template>
  <div
    class="balance__actions d-flex justify-content-evenly py-2"
    :class="cssStyles"
  >
    <button class="btn">Refinance</button>

    <button class="btn">Remove</button>
  </div>
</template>

<style scoped lang="scss">
@import "./balance-card.scss";

.balance__actions {
  &--credit {
    border-top: 1px solid $card-credit-color;
  }

  &--debt {
    border-top: 1px solid $card-debt-color;
  }
}
</style>
