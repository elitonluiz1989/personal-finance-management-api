<script setup="" lang="ts">
import { AppResources } from "@/app/app.resoures";
import { BalanceTypeEnum } from "@/balances/balances-type.enum";
import { BalancesResoures } from "@/balances/balances.resources";
import { computed, defineProps } from "vue";

type BalanceBadgeProps = {
  type: BalanceTypeEnum;
  closed: boolean;
};

const props = defineProps<BalanceBadgeProps>();

const cssStyles = computed(() => ({
  "balance__badges--credit": props.type === BalanceTypeEnum.credit,
  "balance__badges--debt": props.type === BalanceTypeEnum.debt,
}));

const getBalanceType = (type: BalanceTypeEnum): string => {
  if (type === BalanceTypeEnum.credit) return AppResources.credit;

  if (type === BalanceTypeEnum.debt) return AppResources.debt;

  return "";
};
</script>

<template>
  <div class="d-flex flex-column" :class="cssStyles">
    <span class="balance__type badge text-white">
      {{ getBalanceType(props.type) }}
    </span>

    <span class="badge text-bg-dark" v-if="props.closed">
      {{ BalancesResoures.closed }}
    </span>

    <span class="badge text-bg-warning" v-else>
      {{ BalancesResoures.open }}
    </span>
  </div>
</template>

<style scoped lang="scss">
@import "./balance-card.scss";

.badge + .badge {
  margin-top: 0.25rem;
}

.balance__type {
  background-color: $card-credit-type-color;
  border: 1px solid white;
}

.balance__badges {
  &--credit {
    .balance__type {
      background-color: $card-credit-type-color;
    }
  }

  &--debt {
    .balance__type {
      background-color: $card-debt-type-color;
    }
  }
}
</style>
