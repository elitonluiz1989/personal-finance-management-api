import { ActionContext, Module } from "vuex";
import { AuthenticationState } from "./types";
import axios from "axios";
import { State } from "@/app/store/types";
import { isNullOrUndefined, isNullOrWhiteSpace } from "@/app/helpers/helpers";
import { AuthenticationInfo } from "../authentication.model";
import { AuthResources } from "../authentication.resources";
import { AuthenticationDto, AuthenticationRefreshDto } from "../types";

export const authentication: Module<AuthenticationState, State> = {
  namespaced: true,

  state: {
    authentication: undefined,
  },

  getters: {
    [AuthResources.store.getters.authentication.value](
      state: AuthenticationState
    ): AuthenticationInfo | undefined {
      if (
        isNullOrUndefined(state.authentication) === false &&
        isNullOrWhiteSpace(state.authentication.token) === false
      )
        return state.authentication;

      const json = localStorage.getItem(AuthResources.localStorage.authInfo);

      if (isNullOrWhiteSpace(json) || json === null) return undefined;

      return AuthenticationInfo.deserialize(json);
    },

    [AuthResources.store.getters.token.value](
      state: AuthenticationState,
      getters: any
    ): string | undefined {
      const authentication =
        state.authentication ??
        getters[AuthResources.store.getters.authentication.value];

      return authentication?.token;
    },
  },

  mutations: {
    [AuthResources.store.mutations.addAuthInfo.value](
      state: AuthenticationState,
      payload: AuthenticationInfo
    ) {
      state.authentication = payload;
      localStorage.setItem(
        AuthResources.localStorage.authInfo,
        JSON.stringify(payload)
      );
    },
  },

  actions: {
    async [AuthResources.store.actions.login.value](
      { commit },
      payload: AuthenticationDto
    ) {
      const result = await axios.post<AuthenticationInfo>(
        "/Authentication/Authenticate",
        payload
      );

      if (isNullOrUndefined(result.data)) return;

      commit(AuthResources.store.mutations.addAuthInfo.value, result.data);
    },
    async [AuthResources.store.actions.refresh.value](
      context: ActionContext<AuthenticationState, State>,
      payload: AuthenticationRefreshDto
    ) {
      const result = await axios.post<AuthenticationRefreshDto>(
        "/Authentication/Refresh",
        payload
      );

      if (isNullOrUndefined(result.data)) return;

      const authenticationInfo =
        context.getters[AuthResources.store.getters.authentication.namespaced];
      authenticationInfo.token = result.data.token;
      authenticationInfo.refreshToken = result.data.refreshToken;
      authenticationInfo.expires = result.data.expires;

      context.commit(
        AuthResources.store.mutations.addAuthInfo.value,
        authenticationInfo
      );
    },
  },
};
