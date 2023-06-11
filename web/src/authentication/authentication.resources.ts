import { createStoreResourceItem } from "@/app/helpers/helpers";

const namespace = "authentication";

export const AuthResources = {
  refreshToken: {
    confirm:
      "Your session has ended. If you want to restore it, please enter your password again.",
    dataNotFound: "Authentication data not found.",
    tokenNull: "Authentication token is null",
  },
  localStorage: {
    authInfo: "authInfo",
  },
  form: {
    username: "Username",
    password: "Password",
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
