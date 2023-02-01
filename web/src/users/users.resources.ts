import { createStoreResourceItem } from "@/app/helpers/helpers";

const namespace = "users";

export const UsersResoures = {
  store: {
    namespace: namespace,
    getters: {
      users: createStoreResourceItem(namespace, "getUsers"),
    },
    mutations: {
      addUsers: createStoreResourceItem(namespace, "addUsers"),
    },
    actions: {
      list: createStoreResourceItem(namespace, "list"),
    },
  },
};
