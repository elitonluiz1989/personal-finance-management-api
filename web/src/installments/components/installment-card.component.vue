<script setup lang="ts">
import { defineProps, withDefaults } from "vue";
import AppBadge from "@/app/components/bagde/app-badge.component.vue";
import { BootstrapColorsEnum } from "@/app/enums/BootstrapColorsEnum.enum";
import { CurrencyFormatterStatic } from "@/app/helpers/currency-formatter";
import { Installment } from "@/installments/installment.model";
import InstallmentBadge from "@/installments/components/installment-badge.component.vue";

type BalanceCardInstallmentsProp = {
  installment: Installment;
};

const props = defineProps<BalanceCardInstallmentsProp>();
</script>

<template>
  <div class="d-flex w-100 px-2 py-1">
    <div class="d-flex align-items-center">
      <AppBadge
        :color="BootstrapColorsEnum.dark"
        :fit-content="true"
        :text="props.installment.number.toString()"
      />
    </div>

    <div class="balance__installments-reference flex-fill px-3">
      <span>
        {{ props.installment.referenceFormatted }}
      </span>

      <span class="px-2">-</span>

      <span>
        {{ CurrencyFormatterStatic.format(props.installment.amount) }}
      </span>
    </div>

    <InstallmentBadge
      :status="props.installment.status"
      :description="props.installment.statusDescription"
    />
  </div>
</template>
