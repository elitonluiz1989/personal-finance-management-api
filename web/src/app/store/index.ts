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
