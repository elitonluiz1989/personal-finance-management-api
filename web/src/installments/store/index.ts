import { StoreHelper } from "@/app/store/store.helper";
import { State } from "@/app/store/types";
import axios from "axios";
import { Module } from "vuex";
import { Installment } from "../installment.model";
import { InstallmentsResources } from "../installments.resources";
import { InstallmentFilter } from "../models/installment.filter";
import { InstallmentsState } from "./type";
import { PagedResultsDto } from "@/app/models/paged-results.dto";
import { PaginationDto } from "./../../app/models/pagination.dto";

export const installments: Module<InstallmentsState, State> = {
  namespaced: true,

  state: {
    installments: [],
    pagination: {
      page: 1,
    },
  },

  getters: {
    [InstallmentsResources.store.getters.installments.value](
      state: InstallmentsState
    ): Installment[] {
      return state.installments ?? [];
    },
    [InstallmentsResources.store.getters.pagination.page.value](
      state: InstallmentsState
    ): number {
      return state.pagination.page;
    },
  },

  mutations: {
    [InstallmentsResources.store.mutations.addInstallments.value](
      state: InstallmentsState,
      payload: Installment[]
    ) {
      state.installments = Installment.castList(payload);
    },
    [InstallmentsResources.store.mutations.pagination.add.value](
      state: InstallmentsState,
      payload: PaginationDto
    ) {
      state.pagination = payload;
    },
    [InstallmentsResources.store.mutations.pagination.pageIncrement.value](
      state: InstallmentsState
    ) {
      state.pagination.page += 1;
    },
  },

  actions: {
    async [InstallmentsResources.store.actions.list.value](
      { commit },
      payload: InstallmentFilter
    ) {
      const result = await axios.get<PagedResultsDto<Installment>>(
        "/Installments",
        {
          params: payload,
        }
      );

      let installments: Installment[] = result.data.results ?? [];

      if (payload.appendData) {
        const currentInstallments = StoreHelper.get<Installment[]>(
          InstallmentsResources.store.getters.installments.namespaced,
          []
        );

        installments = currentInstallments.concat(installments);
      }

      if (result.data.pagination) {
        commit(
          InstallmentsResources.store.mutations.pagination.add.value,
          result.data.pagination
        );
      }

      commit(
        InstallmentsResources.store.mutations.addInstallments.value,
        installments
      );
    },
  },
};
