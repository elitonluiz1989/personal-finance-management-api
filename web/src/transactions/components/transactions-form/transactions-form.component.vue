<script setup lang="ts">
import AppFormInputCurrency from "@/app/components/form/input-curreny/app-form-input-currency.vue";
import AppModal from "@/app/components/modal/app-modal.component.vue";
import { User } from "@/users/user.model";
import {
  onMounted,
  ref,
  defineProps,
  defineEmits,
  defineExpose,
  withDefaults,
} from "vue";
import { TransactionsResoures } from "../../transactions.resources";
import { Installment } from "@/installments/installment.model";
import { TransactionsFormService } from "./transactions-form.service";

type TransactionsFormProps = {
  showAddButton: boolean;
};
type TransactionFormEmits = {
  (e: "onClose"): void;
};

const show = ref(false);
const modalTitle = ref(TransactionsResoures.form.title.insert);
const editMode = ref(false);
const refinanceMode = ref(false);
const users = ref<User[]>([]);
const installments = ref<Installment[]>();
const props = withDefaults(defineProps<TransactionsFormProps>(), {
  showAddButton: true,
});
const emits = defineEmits<TransactionFormEmits>();

const service = new TransactionsFormService();

const showModal = async (transactionId = 0) => {
  show.value = true;
};
const showModalEvent = async () => {
  await showModal();
};
const closeModal = (value: boolean) => {
  show.value = value;
  editMode.value = false;
  refinanceMode.value = false;
  modalTitle.value = TransactionsResoures.form.title.insert;

  service.form.enableAll();

  service.form.reset();

  emits("onClose");
};
const submitForm = async () => {
  service.form.validate();

  if (service.form.invalid) return;

  closeModal(false);
};

onMounted(async () => {
  users.value = await service.getUsers();
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
    :id="service.modalId"
    :show="show"
    :title="modalTitle"
    :is-form="true"
    :footer="service.modalFooterConfig"
    @on-close="closeModal"
    @on-reset="() => service.form.reset()"
    @on-save.prevent="submitForm"
  >
    <div class="container-fluid">
      <div class="form-group row">
        <div class="col-12 col-sm-5 mb-3">
          <label class="form-label">{{
            TransactionsResoures.form.fields.date
          }}</label>

          <input
            type="date"
            class="form-control"
            form-field="date"
            :disabled="service.form.fields.date.disabled"
            @change="service.form.fields.date.clearValidation"
            v-model="service.form.fields.date.model.value"
          />

          <AppValidationMessages
            :validations="service.form.fields.date.validations"
            v-if="service.form.fields.date.invalid"
          />
        </div>

        <div class="col-12 col-sm-4 mb-3">
          <label class="form-label">
            {{ TransactionsResoures.form.fields.amount }}
          </label>

          <AppFormInputCurrency
            class="form-control"
            form-field="amount"
            :maxlength="14"
            :disabled="service.form.fields.amount.disabled"
            :on-focus="() => service.form.fields.amount.clearValidation()"
            v-model="service.form.fields.amount.model.value"
          />

          <AppValidationMessages
            :validations="service.form.fields.amount.validations"
            v-if="service.form.fields.amount.invalid"
          />
        </div>
      </div>

      <div class="row">
        <div class="col-12 mb-3">
          <label class="form-label">{{
            TransactionsResoures.form.fields.user
          }}</label>

          <select
            class="form-control"
            form-field="user"
            :disabled="service.form.fields.userId.disabled"
            @change="service.form.fields.userId.clearValidation"
            v-model="service.form.fields.userId.model.value"
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
            :validations="service.form.fields.userId.validations"
            v-if="service.form.fields.userId.invalid"
          />
        </div>
      </div>

      <div class="form-group row">
        <div class="col-12 col-sm-4">
          <label class="form-label">
            {{ TransactionsResoures.form.fields.type }}
          </label>

          <select
            class="form-control"
            form-field="type"
            :disabled="service.form.fields.type.disabled"
            @change="service.form.fields.type.clearValidation"
            v-model="service.form.fields.type.model.value"
          >
            <option value="0">...</option>

            <template v-if="service.transactionTypes.length > 0">
              <option
                :value="type.value"
                v-for="(type, index) of service.transactionTypes"
                :key="index"
              >
                {{ type.key }}
              </option>
            </template>
          </select>

          <AppValidationMessages
            :validations="service.form.fields.type.validations"
            v-if="service.form.fields.type.invalid"
          />
        </div>

        <div class="col-12 col-sm">
          <label class="form-label">
            {{ TransactionsResoures.form.fields.installments }}
          </label>

          <select class="form-control" disabled>
            <option value="0">...</option>
          </select>
        </div>
      </div>
    </div>
  </AppModal>
</template>
