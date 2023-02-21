<script setup lang="ts">
import { computed, defineProps, ref } from "vue";
import { Installment } from "@/installments/installment.model";
import { BalanceTypeEnum } from "@/balances/balances-type.enum";
import BalanceCardInstallmentBadge from "./balance-card-installment-badge.component.vue";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { CurrencyFormatterStatic } from "@/app/helpers/currency-formatter";

type BalanceCardInstallmentsProp = {
  installments: Installment[];
  type: BalanceTypeEnum;
};

const props = defineProps<BalanceCardInstallmentsProp>();
const clicked = ref(false);

const cssStyles = computed(() => ({
  "balance__installments--credit": props.type === BalanceTypeEnum.credit,
  "balance__installments--debt": props.type === BalanceTypeEnum.debt,
}));
const angleDownStyles = computed(() => ({
  "balance__installments-indicator--rotated": clicked.value,
}));

const toggleInstallmentsList = () => (clicked.value = !clicked.value);
</script>

<template>
  <div class="balance__installments" :class="cssStyles">
    <button
      class="w-100 py-2 pe-0 d-flex bg-white border-0"
      @click="toggleInstallmentsList"
    >
      <div class="flex-fill ps-3 text-start">Installments</div>

      <div class="balance__installments-indicator" :class="angleDownStyles">
        <FontAwesomeIcon icon="fa-solid fa-plus" />
      </div>
    </button>

    <div class="position-relative">
      <div
        class="balance__installments-floating position-absolute w-100"
        v-if="clicked"
      >
        <div
          class="balance__installments-caret d-flex justify-content-end overflow-hidden"
        >
          <FontAwesomeIcon icon="fa-solid fa-caret-up" size="lg" />
        </div>

        <ul
          class="balance__installments-list ps-0 m-0 rounded bg-white"
          v-if="props.installments.length > 0"
        >
          <li
            class="d-flex list-style-none py-2 px-4"
            v-for="(installment, index) in props.installments"
            :key="index"
          >
            <div class="d-flex align-items-center">
              <span class="badge bg-dark">{{ installment.number }}</span>
            </div>

            <div class="balance__installments-reference flex-fill px-3">
              <span>
                {{ installment.referenceFormatted }}
              </span>

              <span class="px-2">-</span>

              <span>
                {{ CurrencyFormatterStatic.format(installment.amount) }}
              </span>
            </div>

            <BalanceCardInstallmentBadge
              :status="installment.status"
              :description="installment.statusDescription"
            />
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "../balance-card.scss";

.balance__installments {
  $selector: &;

  &-indicator {
    width: 2rem;
    transition: all 0.35s;
    transform: rotate(0);

    &--rotated {
      transform: rotate(45deg);
    }
  }

  &-floating {
    padding-bottom: 1rem;
    z-index: 1;
    top: 0;
    right: 0.3rem;
  }

  &-caret {
    height: 10px;
    padding-right: 0.28rem;
  }

  &-list {
    border-width: 1px;
    border-style: solid;
    box-shadow: 1px 1px 10px #555;

    li {
      border-bottom-width: 1px;
      border-bottom-style: solid;

      &:last-child {
        border-bottom: 0;
      }
    }
  }

  &--credit {
    border-top: 1px solid $card-credit-color;

    #{$selector}-floating {
      border-color: $card-credit-color;
    }

    #{$selector}-caret {
      color: $card-credit-color;
    }

    #{$selector}-list {
      border-color: $card-credit-color;

      li {
        border-bottom-color: $card-credit-color;
      }
    }
  }

  &--debt {
    border-top: 1px solid $card-debt-color;

    #{$selector}-floating {
      border-color: $card-debt-color;
    }

    #{$selector}-caret {
      color: $card-debt-color;
    }

    #{$selector}-list {
      border-color: $card-debt-color;

      li {
        border-bottom-color: $card-debt-color;
      }
    }
  }

  &-reference {
    min-width: 7rem;
  }

  .badge {
    height: fit-content;
  }
}
</style>
