import { Module } from "vuex";
import { BalancesState } from "./types";
import axios from "axios";
import { State } from "@/app/store/types";
import { BalancesResoures } from "../balances.resources";
import { Balance } from "../balance.model";
import { isNullOrUndefined } from "@/app/helpers/helpers";
import { RefinancedBalance } from "../balance-refinanced.model";

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
    [BalancesResoures.store.getters.balances.value](
      state: BalancesState
    ): Balance[] {
      return state.balances ?? [];
    },
    [BalancesResoures.store.getters.balance.value](
      state: BalancesState
    ): (id: number) => Balance | undefined {
      return (id: number) => state.balances.find((b) => b.id === id);
    },
  },

  mutations: {
    [BalancesResoures.store.mutations.addBalanceOnBalances.value](
      state: BalancesState,
      payload: Balance
    ) {
      const balances = state.balances.filter(
        (b: Balance) => b.id !== payload.id
      );
      balances.push(payload);

      handelBalanceData(balances, state);
    },
    [BalancesResoures.store.mutations.addBalances.value](
      state: BalancesState,
      payload: Balance[]
    ) {
      handelBalanceData(payload, state);
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
    async [BalancesResoures.store.actions.find.value](
      { commit },
      payload: number
    ) {
      const result = await axios.get<Balance>(`/Balances/${payload}`);

      if (!result.data) return;

      commit(BalancesResoures.store.mutations.addBalance.value, result.data);
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

      commit(BalancesResoures.store.mutations.addBalances.value, result.data);
    },
    async [BalancesResoures.store.actions.refinance.value](
      { commit, dispatch },
      payload: RefinancedBalance
    ) {
      const result = await axios.post<Balance>("/RefinancedBalances", payload);

      if (isNullOrUndefined(result.data)) {
        dispatch(BalancesResoures.store.actions.list.namespaced);

        return;
      }

      commit(BalancesResoures.store.mutations.addBalances.value, result.data);
    },
    async [BalancesResoures.store.actions.remove.value](
      { state, commit },
      payload: number
    ) {
      const result = await axios.delete<boolean>(`/Balances/${payload}`);

      if (!result || result.data === false) return;

      const balances = state.balances?.filter((b) => b.id !== payload) ?? [];

      commit(BalancesResoures.store.mutations.addBalances.value, balances);
    },
  },
};
