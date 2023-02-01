import { InjectionKey } from "vue";
import { createStore, Store } from "vuex";
import { authentication } from "@/authentication/store";
import { State } from "./types";
import { balances } from "@/balances/store";
import { users } from "@/users/store/index";

export const storeKey: InjectionKey<Store<State>> = Symbol();

export const store = createStore<State>({
  modules: {
    authentication,
    balances,
    users,
  },

  state: {
    isLoading: false,
  },
});
