<script setup lang="ts">
import { defineProps } from "vue";
import AppBadge from "@/app/components/bagde/app-badge.component.vue";
import { InstallmentStatusEnum } from "@/installments/installment-status.enum";
import { BootstrapColorsEnum } from "@/app/enums/BootstrapColorsEnum.enum";

type BalanceCardInstallmentsBadgeProp = {
  status: InstallmentStatusEnum;
  description: string;
};

const props = defineProps<BalanceCardInstallmentsBadgeProp>();
</script>

<template>
  <div class="d-flex align-items-center">
    <AppBadge
      :color="BootstrapColorsEnum.primary"
      :fit-content="true"
      :text="props.description"
      v-if="props.status === InstallmentStatusEnum.created"
    />

    <AppBadge
      :color="BootstrapColorsEnum.success"
      :fit-content="true"
      :text="props.description"
      v-else-if="props.status === InstallmentStatusEnum.paid"
    />

    <AppBadge
      :color="BootstrapColorsEnum.warning"
      :fit-content="true"
      :text="props.description"
      v-else-if="props.status === InstallmentStatusEnum.partiallyPaid"
    />

    <AppBadge
      :color="BootstrapColorsEnum.secondary"
      :fit-content="true"
      :text="props.description"
      v-else
    />
  </div>
</template>
