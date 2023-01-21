import { createStoreResourceItem } from "@/app/helpers/helpers";

const namespace = "authentication";

export const AuthResources = {
  confirmRefreshToken:
    "Your session has ended. If you want to restore it, please enter your password again.",
  localStorage: {
    authInfo: "authInfo",
  },
  store: {
    namespace: namespace,
    getters: {
      authentication: createStoreResourceItem(namespace, "getAuthentication"),
      token: createStoreResourceItem(namespace, "getToken"),
    },
    mutations: {
      addAuthInfo: createStoreResourceItem(namespace, "addAuthenticationInfo"),
    },
    actions: {
      login: createStoreResourceItem(namespace, "login"),
      refresh: createStoreResourceItem(namespace, "refresh"),
    },
  },
};
