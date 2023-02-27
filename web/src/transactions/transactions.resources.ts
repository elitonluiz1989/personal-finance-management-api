import { createStoreResourceItem } from "@/app/helpers/helpers";

const namespace = "transactions";

export const TransactionsResoures = {
  confirm: {
    removeTransaction: "Do you want remove this transaction?",
  },
  form: {
    title: {
      insert: "Add transaction",
      edit: "Edit transaction",
    },
    fields: {
      date: "Date",
      amount: "Amount",
      user: "User",
      type: "Type",
      installments: "Installments",
    },
  },
  store: {
    namespace: namespace,
    getters: {
      Transactions: createStoreResourceItem(namespace, "getTransactions"),
      Transaction: createStoreResourceItem(namespace, "getTransaction"),
    },
    mutations: {
      addTransaction: createStoreResourceItem(namespace, "addTransaction"),
      addTransactions: createStoreResourceItem(namespace, "addTransactions"),
      addTransactionOnTransactions: createStoreResourceItem(
        namespace,
        "addTransactionOnTransactions"
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
