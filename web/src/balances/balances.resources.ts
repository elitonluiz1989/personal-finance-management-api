import { createStoreResourceItem } from "@/app/helpers/helpers";

const namespace = "balances";

export const BalancesResoures = {
  form: {
    title: {
      insert: "Add Balance",
    },
    fields: {
      name: "Name",
      user: "User",
      type: "Type",
      date: "Date",
      amount: "Amount",
      financed: "Financed",
      intallmentsNumber: "Number of installments",
    },
  },
  store: {
    namespace: namespace,
    getters: {
      balances: createStoreResourceItem(namespace, "getBalances"),
    },
    mutations: {
      addBalance: createStoreResourceItem(namespace, "addBalance"),
      addBalances: createStoreResourceItem(namespace, "addBalances"),
    },
    actions: {
      list: createStoreResourceItem(namespace, "list"),
      add: createStoreResourceItem(namespace, "add"),
    },
  },
};
