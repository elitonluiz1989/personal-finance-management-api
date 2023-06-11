<script setup lang="ts">
import { AppResources } from "@/app/app.resoures";
import AppFormInputCurrency from "@/app/components/form/input-curreny/app-form-input-currency.component.vue";
import AppFormInputNumber from "@/app/components/form/input-number/app-form-input-number.component.vue";
import AppValidationMessages from "@/app/components/validation-messages/app-validation-messages.component.vue";
import AppModal from "@/app/components/modal/app-modal.component.vue";
import { IAppModalFooterProps } from "@/app/components/modal/types";
import { KeyValue } from "@/app/types";
import { computed, defineEmits, defineExpose, onMounted, ref } from "vue";
import { User } from "@/users/user.model";
import { BalancesResources } from "@/balances/balances.resources";
import * as BalanceType from "@/balances/balances-type.enum";
import { BalanceFormService } from "./balance-form.service";

type BalanceFormEmits = {
  (e: "onClose"): void;
};

const show = ref(false);
const modalTitle = ref(BalancesResources.form.title.insert);
const users = ref<User[]>([]);

const emits = defineEmits<BalanceFormEmits>();

const modalId = "balance-form-modal";
const modalFooterConfig: IAppModalFooterProps = {
  show: true,
};
const balanceTypes: KeyValue<string, BalanceType.BalanceTypeEnum>[] =
  BalanceType.toKeyValue();
const form = new BalanceFormService();

const intallmentsNumberCssStyles = computed(() => ({
  "installments-number form-group row overflow-hidden show":
    form.fields.financed.model.value,
  "installments-number form-group row overflow-hidden":
    form.fields.financed.model.value === false,
}));
const showFinancedValue = computed(form.verifyIfShowFinancedValue);
const financedValue = computed(form.financedValueFormatted);

const showModal = async (balanceId = 0, refinancing = false) => {
  show.value = true;

  form.resetModes();
  form.reset();

  if (isNaN(balanceId) || +balanceId === 0) return;

  modalTitle.value = BalancesResources.form.title.edit;

  form.enableEditMode(balanceId, refinancing);
};
const showModalEvent = async () => {
  await showModal();
};
const closeModal = (value: boolean) => {
  show.value = value;
  modalTitle.value = BalancesResources.form.title.insert;

  form.resetModes();
  form.enableAll();
  form.reset();

  emits("onClose");
};
const submitForm = async () => {
  await form.submit();

  closeModal(false);
};

onMounted(async () => {
  users.value = await form.getUsers();
});

defineExpose({
  showModal,
});
</script>

<template>
  <button class="btn btn-primary" @click="showModalEvent">Add balance</button>

  <AppModal
    :id="modalId"
    :show="show"
    :title="modalTitle"
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
            BalancesResources.form.fields.name
          }}</label>

          <input
            type="text"
            class="form-control"
            maxlength="100"
            form-field="name"
            :disabled="form.fields.name.disabled"
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
            BalancesResources.form.fields.user
          }}</label>

          <select
            class="form-control"
            form-field="user"
            :disabled="form.fields.userId.disabled"
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
            {{ BalancesResources.form.fields.type }}
          </label>

          <select
            class="form-control"
            form-field="type"
            :disabled="form.fields.type.disabled"
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
            BalancesResources.form.fields.date
          }}</label>

          <input
            type="date"
            class="form-control"
            form-field="date"
            :disabled="form.fields.date.disabled"
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
            {{ BalancesResources.form.fields.amount }}
          </label>

          <AppFormInputCurrency
            class="form-control"
            form-field="amount"
            :maxlength="14"
            :disabled="form.fields.amount.disabled"
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
            {{ BalancesResources.form.fields.financed }}?
            {{ form.fields.financed.model.value }}
          </label>

          <div class="w-100">
            <label class="me-3">
              <input
                type="radio"
                :value="true"
                :disabled="form.fields.financed.disabled"
                v-model="form.fields.financed.model.value"
              />
              {{ AppResources.yes }}
            </label>

            <label>
              <input
                type="radio"
                :value="false"
                :disabled="form.fields.financed.disabled"
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
            {{ BalancesResources.form.fields.intallmentsNumber }}
          </label>
        </div>

        <div class="col-6">
          <AppFormInputNumber
            class="form-control"
            form-field="installmentsNumber"
            :min="1"
            :max="999"
            :disabled="form.fields.installmentsNumber.disabled"
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
