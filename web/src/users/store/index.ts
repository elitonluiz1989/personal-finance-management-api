import { State } from "@/app/store/types";
import axios from "axios";
import { Module } from "vuex";
import { User } from "../user.model";
import { UsersResoures } from "../users.resources";
import { UsersState } from "./types";

export const users: Module<UsersState, State> = {
  namespaced: true,

  state: {
    users: [],
  },

  getters: {
    [UsersResoures.store.getters.users.value](state: UsersState): User[] {
      return state.users ?? [];
    },
  },

  mutations: {
    [UsersResoures.store.mutations.addUsers.value](
      state: UsersState,
      payload: User[]
    ) {
      state.users = payload;
    },
  },

  actions: {
    async [UsersResoures.store.actions.list.value](
      { commit },
      payload: any = null
    ) {
      const result = await axios.get<User[]>("/users", payload);
      const users = result.data ?? [];

      commit(UsersResoures.store.mutations.addUsers.value, users);
    },
  },
};
