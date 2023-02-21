<script setup lang="ts">
import { BalanceTypeEnum } from "@/balances/balances-type.enum";
import { computed, defineEmits, defineProps } from "vue";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

type BalanceCardActionsProps = {
  id: number;
  type: BalanceTypeEnum;
};
type BalanceCardActionEmits = {
  (e: "onEdit"): void;
  (e: "onRefinance"): void;
  (e: "onRemove"): void;
};

const props = defineProps<BalanceCardActionsProps>();
const emits = defineEmits<BalanceCardActionEmits>();

const cssStyles = computed(() => ({
  "balance__actions--credit": props.type === BalanceTypeEnum.credit,
  "balance__actions--debt": props.type === BalanceTypeEnum.debt,
}));

const triggerEdit = () => emits("onEdit");
const triggerRefinance = () => emits("onRefinance");
const triggerRemove = () => emits("onRemove");
</script>

<template>
  <div
    class="balance__actions d-flex justify-content-evenly py-2"
    :class="cssStyles"
  >
    <button title="Refinance" @click="triggerEdit">
      <FontAwesomeIcon icon="fa-solid fa-pen" size="lg" />
    </button>

    <button title="Refinance" @click="triggerRefinance">
      <FontAwesomeIcon icon="fa-solid fa-money-bill-transfer" size="lg" />
    </button>

    <button title="Remove" @click="triggerRemove">
      <FontAwesomeIcon icon="fa-solid fa-trash-can" size="lg" />
    </button>
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

  button {
    padding-top: 0.5rem;
    padding-bottom: 0.5rem;
    background-color: transparent;
    border: 0;
  }
}
</style>
