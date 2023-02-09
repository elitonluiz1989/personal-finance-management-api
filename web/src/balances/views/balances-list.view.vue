<script async setup lang="ts">
import { StoreHelper } from "@/app/store/store.helper";
import { onMounted, ref } from "vue";
import { Balance } from "../balance.model";
import { BalancesResoures } from "../balances.resources";
import BalanceForm from "../components/balance-form.component.vue";
import BalanceCard from "../components/balance-card/balance-card.component.vue";

const balances = ref<Balance[]>([]);

onMounted(async () => {
  await getBalances();
});

const getBalances = async () => {
  await StoreHelper.dispatch(BalancesResoures.store.actions.list.namespaced);

  updateBalances();
};
const updateBalances = () => {
  balances.value = StoreHelper.get<Balance[]>(
    BalancesResoures.store.getters.balances.namespaced,
    []
  );
};
</script>

<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12 d-flex justify-content-end">
        <BalanceForm />
      </div>
    </div>

    <template v-if="balances.length > 0">
      <div
        class="row justify-content-center justify-content-lg-start align-items-stretch py-3"
      >
        <div
          class="col-12 col-sm-6 col-md-5 col-lg-4 col-xl-3 col-xxl-2 mb-3"
          v-for="(balance, index) in balances"
          :key="index"
        >
          <BalanceCard :balance="balance" />
        </div>
      </div>
    </template>

    <div class="row" v-else>
      <div class="col-12">
        <p class="text-center">No records.</p>
      </div>
    </div>
  </div>
</template>
