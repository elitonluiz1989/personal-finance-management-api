<script setup lang="ts">
import AppMessages from "@/app/components/messages/app-messages.component.vue";
import { ValidationRules } from "@/app/services/validation/validation-rules";
import { ValidationService } from "@/app/services/validation/validation.service";
import { AppFom } from "@/app/types";
import { ref } from "vue";
import { AppMessagesHelper } from "@/app/components/messages/app-messages.helpers";
import router from "@/app/router";
import { AuthenticationDto } from "../types";
import { AuthResources } from "../authentication.resources";
import { useStore } from "@/app/store";

const messages = new AppMessagesHelper();
const form: AppFom = {
  username: {
    value: ref(""),
    validations: [ValidationRules.required("Username")],
    invalid: ref(false),
  },
  password: {
    value: ref(""),
    validations: [ValidationRules.required("Password")],
    invalid: ref(false),
  },
};
const validation = new ValidationService();
const store = useStore();

const submitForm = async (evt: Event) => {
  evt.preventDefault();

  messages.reset();

  validation.validate(form);

  if (validation.isInvalid) {
    messages.addErrors(validation.errors);

    return;
  }

  const dto: AuthenticationDto = {
    username: form.username.value.value,
    password: form.password.value.value,
  };

  await store.dispatch(AuthResources.store.actions.login.namespaced, dto);

  router.push("/");
};
</script>

<template>
  <div class="container-fluid">
    <div class="row justify-content-center">
      <div class="col-12 col-sm-7 col-md-6 col-lg-4 col-xl-3 col-xxl-2">
        <form @submit="submitForm">
          <div class="form-group" v-show="messages.content.show">
            <AppMessages
              :messages="messages.content.messages"
              :type="messages.content.type"
            />
          </div>

          <div class="form-group row">
            <label for="username" class="form-label">User</label>

            <input
              type="text"
              autocomplete="username"
              id="username"
              class="form-control"
              v-model="form.username.value.value"
            />
          </div>

          <div class="form-group row">
            <label for="password" class="form-label">Password</label>

            <input
              type="password"
              autocomplete="current-password"
              id="password"
              class="form-control"
              v-model="form.password.value.value"
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
