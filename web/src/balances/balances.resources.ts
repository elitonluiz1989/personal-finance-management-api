import { createStoreResourceItem } from "@/app/helpers/helpers";

const namespace = "balances";

export const BalancesResoures = {
  open: "Open",
  closed: "Closed",
  confirm: {
    removeBalance: "Do you want remove this balance?",
  },
  form: {
    title: {
      insert: "Add Balance",
      edit: "Edit Balance",
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
      balance: createStoreResourceItem(namespace, "getBalance"),
    },
    mutations: {
      addBalance: createStoreResourceItem(namespace, "addBalance"),
      addBalances: createStoreResourceItem(namespace, "addBalances"),
      addBalanceOnBalances: createStoreResourceItem(
        namespace,
        "addBalanceOnBalances"
      ),
    },
    actions: {
      list: createStoreResourceItem(namespace, "list"),
      find: createStoreResourceItem(namespace, "find"),
      add: createStoreResourceItem(namespace, "add"),
      refinance: createStoreResourceItem(namespace, "refinance"),
      remove: createStoreResourceItem(namespace, "remove"),
    },
  },
};
