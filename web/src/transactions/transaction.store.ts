import { State } from "@/app/store/types";
import axios from "axios";
import { Module } from "vuex";
import { Transaction } from "./models/transaction.model";
import { TransactionStore } from "./transaction.types";
import { TransactionsStoreStrings as Resources } from "./transactions.resources";
import { TransactionStoreDto } from "./models/transaction-store.dto";

export const transactions: Module<TransactionStore, State> = {
  namespaced: true,

  state: {
    transactions: [],
  },

  getters: {
    [Resources.getterTransaction.value](
      state: TransactionStore
    ): Transaction[] {
      return state.transactions ?? [];
    },
  },

  mutations: {
    [Resources.setterAddTransactions.value](
      state: TransactionStore,
      payload: Transaction[]
    ) {
      state.transactions = [];

      for (const item of payload) {
        if (item instanceof Transaction) {
          state.transactions.push(item);

          continue;
        }

        state.transactions.push(Transaction.createFrom(item));
      }
    },

    [Resources.setterAddTransaction.value](
      state: TransactionStore,
      payload: Transaction
    ) {
      const transaction = Transaction.createFrom(payload);
      const key = state.transactions.findIndex((t) => t.id === payload.id);

      if (state.transactions[key]) {
        state.transactions[key] = transaction;
      } else {
        state.transactions.push(payload);
      }
    },
  },

  actions: {
    async [Resources.actionList.value](
      { commit },
      payload: any
    ): Promise<void> {
      const result = await axios.get<Transaction>("/Transactions", payload);
      const transactions = result?.data ?? [];

      commit(Resources.setterAddTransactions.value, transactions);
    },

    async [Resources.actionAdd.value](
      { commit, dispatch },
      payload: TransactionStoreDto
    ): Promise<void> {
      const result = await axios.post<Transaction>("/Transactions", payload);

      if (!result.data) {
        dispatch(Resources.actionList.namespaced);

        return;
      }

      commit(Resources.setterAddTransaction.value, result.data);
    },
  },
};
