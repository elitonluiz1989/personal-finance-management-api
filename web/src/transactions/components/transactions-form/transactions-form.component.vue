<script setup lang="ts">
import AppFormInputCurrency from "@/app/components/form/input-curreny/app-form-input-currency.component.vue";
import AppModal from "@/app/components/modal/app-modal.component.vue";
import { User } from "@/users/user.model";
import {
  onMounted,
  ref,
  defineProps,
  defineEmits,
  defineExpose,
  withDefaults,
  computed,
} from "vue";
import { TransactionsResources } from "../../transactions.resources";
import { Installment } from "@/installments/installment.model";
import { TransactionsFormService } from "./transactions-form.service";
import TransactionsFormInstallments from "./installments/transactions-form-installments.component.vue";
import { CurrencyFormatterStatic } from "@/app/helpers/currency-formatter";

type TransactionsFormProps = {
  showAddButton: boolean;
};
type TransactionFormEmits = {
  (e: "onClose"): void;
};

const show = ref(false);
const modalTitle = ref(TransactionsResources.form.title.insert);
const editMode = ref(false);
const refinanceMode = ref(false);
const users = ref<User[]>([]);
const transactionInstallmentsAmount = ref(0);
const allowKeepSelectionInstalments = ref(true);

const props = withDefaults(defineProps<TransactionsFormProps>(), {
  showAddButton: true,
});
const emits = defineEmits<TransactionFormEmits>();

const form = new TransactionsFormService();

const transactionInstallmentsAmountFormatted = computed((): string => {
  if (!transactionInstallmentsAmount.value)
    return CurrencyFormatterStatic.format(0);

  return CurrencyFormatterStatic.format(transactionInstallmentsAmount.value);
});

const onChangeAmount = async (): Promise<void> => {
  if (form.fields.amount.changed) {
    form.fields.userId.enable();
    form.fields.type.enable();

    validateIfAllowKeepSelection();
  } else {
    form.fields.userId.disable();
    form.fields.type.disable();
  }
};
const onChangeUser = async (): Promise<void> => {
  form.fields.userId.clearValidation();

  await searchInstallments();
};
const onChangeType = async (): Promise<void> => {
  form.fields.type.clearValidation();

  await searchInstallments();
};
const searchInstallments = async (appendData = false): Promise<void> => {
  if (
    form.fields.userId.changed === false ||
    form.fields.type.changed === false
  ) {
    form.clearStatedInstallments();

    return;
  }

  if (appendData === false) form.clearStatedInstallments();

  await form.searchForUserInstallments(
    form.fields.userId.value,
    form.fields.type.value,
    appendData
  );
};
const validateIfAllowKeepSelection = (): void => {
  allowKeepSelectionInstalments.value =
    transactionInstallmentsAmount.value < form.fields.amount.value;
};
const selectedInstallmentHandler = (installment: Installment): void => {
  const alreadySelected = form.fields.installments.value.find(
    (i: Installment) => i.id === installment.id
  );

  if (alreadySelected) {
    form.fields.installments.model.value =
      form.fields.installments.model.value.filter(
        (i: Installment) => i.id !== installment.id
      );
  } else {
    form.fields.installments.model.value.push(installment);
  }

  transactionInstallmentsAmount.value =
    form.fields.installments.model.value.reduce(
      (acc: number, installment: Installment) => acc + installment.amount,
      0
    );

  validateIfAllowKeepSelection();
};
const showModal = (transactionId = 0): void => {
  show.value = true;
};
const showModalEvent = async (): Promise<void> => {
  await showModal();
};
const closeModal = (value: boolean): void => {
  show.value = value;
  editMode.value = false;
  refinanceMode.value = false;
  modalTitle.value = TransactionsResources.form.title.insert;
  transactionInstallmentsAmount.value = 0;

  form.clearStatedInstallments();
  form.enableAll();
  form.reset();

  emits("onClose");
};
const submitForm = async (): Promise<void> => {
  form.validate();

  if (form.invalid) return;

  closeModal(false);
};

onMounted(async (): Promise<void> => {
  users.value = await form.getUsers();
});

defineExpose({
  showModal,
});
</script>

<template>
  <button
    class="btn btn-primary"
    @click="showModalEvent"
    v-if="props.showAddButton"
  >
    Add Transaction
  </button>

  <AppModal
    :id="form.modalId"
    :show="show"
    :title="modalTitle"
    :is-form="true"
    :footer="form.modalFooterConfig"
    @on-close="closeModal"
    @on-reset="() => form.reset()"
    @on-save.prevent="submitForm"
  >
    <div class="container-fluid">
      <div class="form-group row">
        <div class="col-12 col-sm-5 mb-3">
          <label class="form-label">{{
            TransactionsResources.form.fields.date
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

        <div class="col-12 col-sm-4 mb-3">
          <label class="form-label">
            {{ TransactionsResources.form.fields.amount }}
          </label>

          <AppFormInputCurrency
            class="form-control text-end"
            form-field="amount"
            :maxlength="14"
            :disabled="form.fields.amount.disabled"
            :on-focus="() => form.fields.amount.clearValidation()"
            :on-key-up="onChangeAmount"
            v-model="form.fields.amount.model.value"
          />

          <AppValidationMessages
            :validations="form.fields.amount.validations"
            v-if="form.fields.amount.invalid"
          />
        </div>
      </div>

      <div class="row">
        <div class="col-12 col-sm mb-3">
          <label class="form-label">{{
            TransactionsResources.form.fields.user
          }}</label>

          <select
            class="form-control"
            form-field="user"
            :disabled="form.fields.userId.disabled"
            @change="onChangeUser"
            v-model="form.fields.userId.model.value"
          >
            <option :value="(0 as number)">...</option>

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

        <div class="col-12 col-sm-4">
          <label class="form-label">
            {{ TransactionsResources.form.fields.type }}
          </label>

          <select
            class="form-control"
            form-field="type"
            :disabled="form.fields.type.disabled"
            @change="onChangeType"
            v-model="form.fields.type.model.value"
          >
            <option value="0">...</option>

            <template v-if="form.transactionTypes.length > 0">
              <option
                :value="type.value"
                v-for="(type, index) of form.transactionTypes"
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
      </div>

      <div class="form-group row">
        <div class="col-12">
          <div class="d-flex mb-3">
            <label class="fw-bold"> Amount of selected installments: </label>

            <span
              class="ms-2 form-control flex-fill w-fit-content text-end disabled"
            >
              {{ transactionInstallmentsAmountFormatted }}
            </span>
          </div>

          <TransactionsFormInstallments
            :allow-selection="allowKeepSelectionInstalments"
            @on-search="searchInstallments"
            @on-select-installment="selectedInstallmentHandler"
          />
        </div>
      </div>
    </div>
  </AppModal>
</template>
