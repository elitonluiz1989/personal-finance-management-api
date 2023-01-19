const namespace = "authentication";

type StoreResourcesItem = {
  value: string;
  namespaced: string;
};

function createStoreItem(value: string): StoreResourcesItem {
  return {
    value: value,
    namespaced: `${namespace}/${value}`,
  };
}

export const AuthResources = {
  confirmRefreshToken: "Your session ended, do you want to restore it?",
  localStorage: {
    authInfo: "authInfo",
  },
  store: {
    namespace: namespace,
    getters: {
      authentication: createStoreItem("getAuthentication"),
    },
    mutations: {
      addAuthInfo: createStoreItem("addAuthenticationInfo"),
    },
    actions: {
      login: createStoreItem("login"),
      refresh: createStoreItem("refresh"),
    },
  },
};
