import { createStoreResourceItem } from "@/app/helpers/helpers";

const namespace = "installments";

export const InstallmentsResources = {
  store: {
    namespace: namespace,
    getters: {
      installments: createStoreResourceItem(namespace, "getInstallments"),
      pagination: {
        page: createStoreResourceItem(namespace, "paginationPage"),
      },
    },
    mutations: {
      addInstallments: createStoreResourceItem(namespace, "addInstallments"),
      pagination: {
        add: createStoreResourceItem(namespace, "pagination"),
        pageIncrement: createStoreResourceItem(namespace, "paginationPage"),
      },
    },
    actions: {
      list: createStoreResourceItem(namespace, "list"),
    },
  },
};
