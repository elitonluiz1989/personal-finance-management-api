<script setup lang="ts">
import { defineProps } from "vue";
import { InstallmentStatusEnum } from "@/installments/installment-status.enum";

type BalanceCardInstallmentsBadgeProp = {
  status: InstallmentStatusEnum;
  description: string;
};

const props = defineProps<BalanceCardInstallmentsBadgeProp>();
</script>

<template>
  <div class="d-flex align-items-center">
    <span
      class="badge bg-primary"
      v-if="props.status === InstallmentStatusEnum.Created"
    >
      {{ props.description }}
    </span>

    <span
      class="badge bg-success"
      v-else-if="props.status === InstallmentStatusEnum.Paid"
    >
      {{ props.description }}
    </span>

    <span
      class="badge bg-warning"
      v-else-if="props.status === InstallmentStatusEnum.PartiallyPaid"
    >
      {{ props.description }}
    </span>

    <span class="badge bg-secondary" v-else>
      {{ props.description }}
    </span>
  </div>
</template>

<style scoped>
.badge {
  height: fit-content;
}
</style>
