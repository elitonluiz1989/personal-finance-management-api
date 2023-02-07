import { Module } from "vuex";
import { BalancesState } from "./types";
import axios from "axios";
import { State } from "@/app/store/types";
import { BalancesResoures } from "../balances.resources";
import { Balance } from "../balance.model";
import { isNullOrUndefined } from "@/app/helpers/helpers";

const handelBalanceData = (balances: Balance[], state: BalancesState) => {
  for (const key in balances) {
    balances[key] = Balance.createFrom(balances[key]);
  }

  state.balances = balances;
};

export const balances: Module<BalancesState, State> = {
  namespaced: true,

  state: {
    balances: [],
  },

  getters: {
    [BalancesResoures.store.getters.balances.value](state: BalancesState): any {
      return state.balances ?? [];
    },
  },

  mutations: {
    [BalancesResoures.store.mutations.addBalances.value](
      state: BalancesState,
      payload: Balance[]
    ) {
      handelBalanceData(payload, state);
    },
    [BalancesResoures.store.mutations.addBalance.value](
      state: BalancesState,
      payload: Balance
    ) {
      const balances = state.balances.filter(
        (b: Balance) => b.id !== payload.id
      );
      balances.push(payload);

      handelBalanceData(balances, state);
    },
  },

  actions: {
    async [BalancesResoures.store.actions.list.value](
      { commit },
      payload: any
    ) {
      const result = await axios.get<Balance[]>("/Balances", payload);
      const balances = result.data ?? [];

      commit(BalancesResoures.store.mutations.addBalances.value, balances);
    },
    async [BalancesResoures.store.actions.add.value](
      { commit, dispatch },
      payload: Balance
    ) {
      const result = await axios.post<Balance>("/Balances", payload);

      if (isNullOrUndefined(result.data)) {
        dispatch(BalancesResoures.store.actions.list.namespaced);

        return;
      }

      commit(BalancesResoures.store.mutations.addBalance.value, result.data);
    },
  },
};
