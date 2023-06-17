<script setup lang="ts">
import { computed, defineEmits, defineProps, withDefaults } from "vue";
import { Installment } from "@/installments/installment.model";
import { TransactionsFormStrings as FormStrings } from "@/transactions/transactions.resources";
import { StoreHelper } from "@/app/store/store.helper";
import { InstallmentsResources } from "@/installments/installments.resources";
import TransactionsFormInstallment from "./transactions-form-installment.component.vue";

type TransactionsFormInstallmentsPropsType = {
  allowSelection: boolean;
};
type TransactionsFormInstallmentsEmitsType = {
  (e: "onSearch", appendData: boolean): void;
  (e: "onSelectInstallment", installmentId: Installment): void;
};

const props = withDefaults(
  defineProps<TransactionsFormInstallmentsPropsType>(),
  {
    allowSelection: true,
  }
);
const emits = defineEmits<TransactionsFormInstallmentsEmitsType>();

const installments = computed(() =>
  StoreHelper.get<Installment[]>(
    InstallmentsResources.store.getters.installments.namespaced,
    []
  )
);

const search = (appendData: boolean) => emits("onSearch", appendData);
const selectInstallment = (installment: Installment) =>
  emits("onSelectInstallment", installment);
</script>

<template>
  <div v-if="installments?.length > 0">
    <div class="card overflow-hidden mb-1">
      <ul class="installments w-100 list-unstyled pt-1 ps-1 pb-3 pe-1 m-0">
        <li
          class="mt-2"
          v-for="(installment, index) in installments"
          :key="index"
        >
          <TransactionsFormInstallment
            :installment="installment"
            :allow-selection="props.allowSelection"
            @on-select-installment="selectInstallment"
          />
        </li>
      </ul>
    </div>

    <div>
      <button
        class="btn btn-outline-secondary w-100"
        @click.prevent="search(true)"
      >
        {{ FormStrings.loadMore }}
      </button>
    </div>
  </div>
</template>

<style scoped>
.installments {
  max-height: 23rem;
  overflow-y: scroll;
}
</style>
