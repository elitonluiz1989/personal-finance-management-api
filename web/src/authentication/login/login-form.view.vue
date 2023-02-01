<script setup lang="ts">
import router from "@/app/router";
import { FormService } from "@/app/services/form/form.service";
import {
  FormOptions,
  FormFieldsOptions,
  FormFields,
} from "@/app/services/form/types";
import { ValidationRules } from "@/app/services/validation/validation-rules";
import { AuthResources } from "../authentication.resources";
import { AuthenticationDto } from "../types";
import AppValidationMessages from "@/app/components/validation-messages/app-validation-messages.component.vue";

const formFieldsOptions: FormFieldsOptions = {
  username: {
    initialValue: "",
    validations: [ValidationRules.required("Username")],
  },
  password: {
    initialValue: "",
    validations: [ValidationRules.required("Password")],
  },
};
const formOptions: FormOptions = {
  fields: formFieldsOptions,
  submit: {
    action: AuthResources.store.actions.login.namespaced,
    dataHandler: (fields: FormFields): AuthenticationDto => ({
      username: fields.username.model.value,
      password: fields.password.model.value,
    }),
  },
};

const form = new FormService(formOptions);

const submitForm = async () => {
  form.validate();

  if (form.invalid) return;

  await form.submit();

  router.push("/");
};
</script>

<template>
  <div class="container-fluid">
    <div class="row justify-content-center">
      <div class="col-12 col-sm-7 col-md-6 col-lg-4 col-xl-3 col-xxl-2">
        <form @submit.prevent="submitForm" @reset="() => form.reset()">
          <div class="form-group row">
            <label class="form-label">User</label>

            <input
              type="text"
              autocomplete="username"
              class="form-control"
              form-field="username"
              @focus="form.fields.username.clearValidation"
              v-model="form.fields.username.model.value"
            />

            <AppValidationMessages
              :validations="form.fields.username.validations"
              v-if="form.fields.username.invalid"
            />
          </div>

          <div class="form-group row">
            <label for="password" class="form-label">Password</label>

            <input
              type="password"
              autocomplete="current-password"
              class="form-control"
              form-field="password"
              @focus="form.fields.password.clearValidation"
              v-model="form.fields.password.model.value"
            />

            <AppValidationMessages
              :validations="form.fields.password.validations"
              v-if="form.fields.password.invalid"
            />
          </div>

          <div class="form-group mt-2 d-flex justify-content-evenly">
            <input
              type="reset"
              class="btn btn-outline-secondary"
              value="Reset"
            />

            <input type="submit" class="btn btn-secondary" value="Save" />
          </div>
        </form>
      </div>
    </div>
  </div>
</template>
