<script setup lang="ts">
import { AppResources } from "@/app/app.resoures";
import AppFormInputCurrency from "@/app/components/form/input-curreny/app-form-input-currency.vue";
import AppFormInputNumber from "@/app/components/form/input-number/app-form-input-number.component.vue";
import AppValidationMessages from "@/app/components/validation-messages/app-validation-messages.component.vue";
import AppModal from "@/app/components/modal/app-modal.component.vue";
import { IAppModalFooterProps } from "@/app/components/modal/types";
import { ValidationRules } from "@/app/services/validation/validation-rules";
import { KeyValue } from "@/app/types";
import { computed, onMounted, ref } from "vue";
import * as BalanceType from "../balances-type.enum";
import { BalancesResoures } from "../balances.resources";
import { FormService } from "@/app/services/form/form.service";
import {
  FormFields,
  FormFieldsOptions,
  FormOptions,
} from "@/app/services/form/types";
import { StoreHelper } from "@/app/store/store.helper";
import { UsersResoures } from "@/users/users.resources";
import { User } from "@/users/user.model";
import { Balance } from "../balance.model";

type FormDataHandlerType = (fields: FormFields) => Balance;

const showModal = ref(false);
const modalId = "balance-form-modal";
const modalFooterConfig: IAppModalFooterProps = {
  show: true,
};
const balanceTypes: KeyValue<string, BalanceType.BalanceTypeEnum>[] =
  BalanceType.toKeyValue();
const users = ref<User[]>([]);
const formFieldsOptions: FormFieldsOptions = {
  name: {
    initialValue: "",
    validations: [
      ValidationRules.required<string>(BalancesResoures.form.fields.name),
      ValidationRules.maxlength(BalancesResoures.form.fields.name, 100),
    ],
  },
  userId: {
    initialValue: 0,
    validations: [
      ValidationRules.required<number>(BalancesResoures.form.fields.user),
    ],
  },
  type: {
    initialValue: 0,
    validations: [
      ValidationRules.required<number>(BalancesResoures.form.fields.type),
    ],
  },
  date: {
    initialValue: "",
    validations: [
      ValidationRules.required<string>(BalancesResoures.form.fields.date),
    ],
  },
  amount: {
    initialValue: 0,
    validations: [
      ValidationRules.required<number>(BalancesResoures.form.fields.amount),
    ],
  },
  financed: {
    initialValue: false,
    validations: [
      ValidationRules.required<boolean>(BalancesResoures.form.fields.financed),
    ],
  },
  installmentsNumber: {
    initialValue: 0,
    validations: [
      ValidationRules.requiredIf<number>(
        BalancesResoures.form.fields.intallmentsNumber,
        () => form.fields.financed.value
      ),
      ValidationRules.between(
        BalancesResoures.form.fields.intallmentsNumber,
        1,
        999
      ),
    ],
  },
};
const dataHandler: FormDataHandlerType = (fields: FormFields): Balance => {
  const balance = new Balance();
  balance.userId = fields.userId.value;
  balance.name = fields.name.value;
  balance.type = fields.type.value;
  balance.date = fields.date.value;
  balance.amount = fields.amount.value;
  balance.financed = fields.financed.value;

  if (balance.financed)
    balance.installmentsNumber = form.fields.installmentsNumber.model.value;

  return balance;
};
const formOptions: FormOptions = {
  fields: formFieldsOptions,
  submit: {
    action: BalancesResoures.store.actions.add.namespaced,
    dataHandler: dataHandler,
  },
};
const form = new FormService(formOptions);

const intallmentsNumberCssStyles = computed(() => ({
  "installments-number form-group row overflow-hidden show":
    form.fields.financed.model.value,
  "installments-number form-group row overflow-hidden":
    form.fields.financed.model.value === false,
}));

onMounted(async () => {
  users.value = await getUsers();
});

const showFinancedValue = computed(() => {
  return (
    isNaN(form.fields.amount.value) === false &&
    isNaN(form.fields.installmentsNumber.value) === false &&
    form.fields.amount.value > 0 &&
    form.fields.installmentsNumber.value > 0
  );
});
const financedValue = computed(() => {
  if (showFinancedValue.value === false) return 0;

  const financedValue =
    form.fields.amount.value / form.fields.installmentsNumber.value;

  return financedValue.toFixed(2);
});

const getUsers = async (): Promise<User[]> => {
  await StoreHelper.dispatch(UsersResoures.store.actions.list.namespaced, {});

  return StoreHelper.get<User[]>(
    UsersResoures.store.getters.users.namespaced,
    []
  );
};
const showModalEvent = () => (showModal.value = true);
const closeModal = (show: boolean) => {
  showModal.value = show;

  form.reset();
};
const submitForm = async () => {
  form.validate();

  if (form.invalid) return;

  await form.submit();

  closeModal(false);
};
</script>

<template>
  <button class="btn btn-primary" @click="showModalEvent">Add balance</button>

  <AppModal
    :id="modalId"
    :show="showModal"
    :title="BalancesResoures.form.title.insert"
    :is-form="true"
    :footer="modalFooterConfig"
    @on-close="closeModal"
    @on-reset="() => form.reset()"
    @on-save.prevent="submitForm"
  >
    <div class="container-fluid">
      <div class="form-group row">
        <div class="col-12 mb-3">
          <label class="form-label">{{
            BalancesResoures.form.fields.name
          }}</label>

          <input
            type="text"
            class="form-control"
            maxlength="100"
            form-field="name"
            @focus="form.fields.name.clearValidation"
            v-model="form.fields.name.model.value"
          />

          <AppValidationMessages
            :validations="form.fields.name.validations"
            v-if="form.fields.name.invalid"
          />
        </div>
      </div>

      <div class="row">
        <div class="col-12 mb-3">
          <label class="form-label">{{
            BalancesResoures.form.fields.user
          }}</label>

          <select
            class="form-control"
            form-field="user"
            @change="form.fields.userId.clearValidation"
            v-model="form.fields.userId.model.value"
          >
            <option value="0">...</option>

            <template v-if="users.length > 0">
              <option
                :value="user.id"
                v-for="(user, index) of users"
                :key="index"
              >
                {{ user.name }}
              </option>
            </template>
          </select>

          <AppValidationMessages
            :validations="form.fields.userId.validations"
            v-if="form.fields.userId.invalid"
          />
        </div>
      </div>

      <div class="form-group row">
        <div class="col-12 col-sm-4 mb-3">
          <label class="form-label">
            {{ BalancesResoures.form.fields.type }}
          </label>

          <select
            class="form-control"
            form-field="type"
            @change="form.fields.type.clearValidation"
            v-model="form.fields.type.model.value"
          >
            <option value="0">...</option>

            <template v-if="balanceTypes.length > 0">
              <option
                :value="type.value"
                v-for="(type, index) of balanceTypes"
                :key="index"
              >
                {{ type.key }}
              </option>
            </template>
          </select>

          <AppValidationMessages
            :validations="form.fields.type.validations"
            v-if="form.fields.type.invalid"
          />
        </div>

        <div class="col-12 col-sm-5 mb-3">
          <label class="form-label">{{
            BalancesResoures.form.fields.date
          }}</label>

          <input
            type="date"
            class="form-control"
            form-field="date"
            @change="form.fields.date.clearValidation"
            v-model="form.fields.date.model.value"
          />

          <AppValidationMessages
            :validations="form.fields.date.validations"
            v-if="form.fields.date.invalid"
          />
        </div>
      </div>

      <div class="form-group row">
        <div class="col-12 col-sm-4 mb-3">
          <label class="form-label">
            {{ BalancesResoures.form.fields.amount }}
          </label>

          <AppFormInputCurrency
            class="form-control"
            form-field="amount"
            :maxlength="14"
            :on-focus="() => form.fields.amount.clearValidation()"
            v-model="form.fields.amount.model.value"
          />

          <AppValidationMessages
            :validations="form.fields.amount.validations"
            v-if="form.fields.amount.invalid"
          />
        </div>

        <div class="col-12 col-sm-8 mb-3">
          <label class="form-label">
            {{ BalancesResoures.form.fields.financed }}?
          </label>

          <div class="w-100">
            <label class="me-3">
              <input
                type="radio"
                :value="true"
                v-model="form.fields.financed.model.value"
              />
              {{ AppResources.yes }}
            </label>

            <label>
              <input
                type="radio"
                :value="false"
                v-model="form.fields.financed.model.value"
              />
              {{ AppResources.no }}
            </label>
          </div>

          <AppValidationMessages
            :validations="form.fields.financed.validations"
            v-if="form.fields.financed.invalid"
          />
        </div>
      </div>

      <div :class="intallmentsNumberCssStyles">
        <div class="col-12">
          <label class="form-label">
            {{ BalancesResoures.form.fields.intallmentsNumber }}
          </label>
        </div>

        <div class="col-6">
          <AppFormInputNumber
            class="form-control"
            form-field="installmentsNumber"
            :min="1"
            :max="999"
            :on-focus="() => form.fields.installmentsNumber.clearValidation()"
            v-model="form.fields.installmentsNumber.model.value"
          />
        </div>

        <div class="col-12" v-if="showFinancedValue">
          <div class="mx-1 my-2 px-2 py-3 bg-secondary rounded-3 text-white">
            {{ form.fields.installmentsNumber.value }} x {{ financedValue }}
          </div>
        </div>

        <AppValidationMessages
          :validations="form.fields.installmentsNumber.validations"
          v-if="form.fields.installmentsNumber.invalid"
        />
      </div>
    </div>
  </AppModal>
</template>

<style scoped lang="scss">
.installments-number {
  height: 0;
  opacity: 0;
  transition: height 0.15s linear, opacity 0.15s linear;

  &.show {
    height: fit-content;
    opacity: 1;
  }
}
</style>
