<script setup lang="ts">
import AppFormInputCurrency from "@/app/components/form/input-curreny/app-form-input-currency.component.vue";
import AppModal from "@/app/components/modal/app-modal.component.vue";
import AppValidationMessages from "@/app/components/validation-messages/app-validation-messages.component.vue";
import { Installment } from "@/installments/installment.model";
import {
  computed,
  defineEmits,
  defineExpose,
  defineProps,
  onMounted,
  withDefaults,
} from "vue";
import { TransactionsFormStrings as FormStrings } from "../../transactions.resources";
import TransactionsFormInstallments from "./installments/transactions-form-installments.component.vue";
import { TransactionsFormEventsService } from "./services/transaction-form-events.service";
import { TransactionsFormService } from "./services/transactions-form.service";

type TransactionsFormProps = {
  showAddButton?: boolean;
};
type TransactionFormEmits = {
  (e: "onClose"): void;
};

const props = withDefaults(defineProps<TransactionsFormProps>(), {
  showAddButton: true,
});
const emits = defineEmits<TransactionFormEmits>();

const form = new TransactionsFormService();
const events = new TransactionsFormEventsService(form);

const installmentsAmountFormatted = computed((): string =>
  form.getInstallmentsAmountFormatted()
);
const modalTitle = computed((): string => form.getModalTitle());

const showModal = (): void => {
  form.show.value = true;
};
const closeModal = (): void => {
  events.closeModalHandler();

  emits("onClose");
};
const submitForm = async (): Promise<void> => {
  form.submit();

  closeModal();
};

onMounted(async (): Promise<void> => {
  await events.populateUsers();
});

defineExpose({
  showModal,
});
</script>

<template>
  <button class="btn btn-primary" @click="showModal" v-if="props.showAddButton">
    Add Transaction
  </button>

  <AppModal
    :show="form.show.value"
    :title="modalTitle"
    :is-form="true"
    :footer="form.modalFooterConfig"
    @on-close="closeModal"
    @on-reset="() => events.reset()"
    @on-save.prevent="submitForm"
  >
    <div class="container-fluid">
      <div class="form-group row">
        <div class="col-12 col-sm-5 mb-3">
          <label class="form-label">{{ FormStrings.date }}</label>

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
            {{ FormStrings.amount }}
          </label>

          <AppFormInputCurrency
            class="form-control text-end"
            form-field="amount"
            :maxlength="14"
            :disabled="form.fields.amount.disabled"
            @on-focus="() => form.fields.amount.clearValidation()"
            @on-key-up="() => events.amountChangeHandler()"
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
          <label class="form-label">{{ FormStrings.user }}</label>

          <select
            class="form-control"
            form-field="user"
            :disabled="form.fields.userId.disabled"
            @change="events.userChangeHandler"
            v-model="form.fields.userId.model.value"
          >
            <option :value="(0 as number)">...</option>

            <template v-if="form.users.value.length > 0">
              <option
                :value="user.id"
                v-for="(user, index) of form.users.value"
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
            {{ FormStrings.type }}
          </label>

          <select
            class="form-control"
            form-field="type"
            :disabled="form.fields.type.disabled"
            @change="events.typeChangeHandler"
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
          <div class="flex-mb-3">
            <div class="d-flex">
              <label class="fw-bold"> Amount of selected installments: </label>

              <span
                class="ms-2 form-control flex-fill w-fit-content text-end disabled"
              >
                {{ installmentsAmountFormatted }}
              </span>
            </div>

            <AppValidationMessages
              :validations="form.fields.installments.validations"
              v-if="form.fields.installments.invalid"
            />
          </div>

          <TransactionsFormInstallments
            :allow-selection="form.allowKeepSelectionInstalments.value"
            @on-search="events.searchInstallments"
            @on-select-installment="(installment: Installment) => events.selectedInstallmentHandler(installment)"
          />
        </div>
      </div>
    </div>
  </AppModal>
</template>
