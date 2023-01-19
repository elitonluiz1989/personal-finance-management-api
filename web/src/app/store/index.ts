import { InjectionKey } from "vue";
import { createStore, useStore as useStoreBase, Store } from "vuex";
import { authentication } from "@/authentication/store";
import { State } from "./types";

export const storeKey: InjectionKey<Store<State>> = Symbol();

export const store = createStore<State>({
  modules: {
    authentication,
  },

  state: {
    isLoading: false,
  },
});

export function useStore() {
  return useStoreBase(storeKey) ?? store;
}

export function storeGet<T>(getter: string): T {
  const store = useStore();

  return store?.getters[getter] as T;
}
