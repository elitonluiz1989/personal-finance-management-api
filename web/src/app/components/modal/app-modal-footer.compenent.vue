<script setup lang="ts">
import { AppResources } from "@/app/app.resoures";
import { computed, withDefaults, defineProps, defineEmits } from "vue";
import { IAppModalFooterProps } from "./types";

type AppModalFooterProps = {
  isForm: boolean;
  saveText?: string;
  resetText?: string;
  dismissText?: string;
};
type AppModalFooterEmits = {
  (e: "triggerOnClose"): void;
};

const props = withDefaults(defineProps<AppModalFooterProps>(), {
  isForm: false,
});

const defaultFooterConfigs: IAppModalFooterProps = {
  show: false,
  saveText: AppResources.save,
  resetText: AppResources.reset,
  dismissText: AppResources.close,
};

const saveText = computed(
  () => props.saveText ?? defaultFooterConfigs.saveText
);
const resetText = computed(
  () => props.resetText ?? defaultFooterConfigs.resetText
);
const dismissText = computed(
  () => props.dismissText ?? defaultFooterConfigs.dismissText
);
const saveButtonType = computed(() => (props.isForm ? "submit" : "button"));

const emits = defineEmits<AppModalFooterEmits>();

const closeModal = () => emits("triggerOnClose");
</script>

<template>
  <div class="modal-footer">
    <button
      type="button"
      class="btn btn-secondary"
      data-dismiss="modal"
      @click="closeModal"
    >
      {{ dismissText }}
    </button>

    <button
      type="reset"
      class="btn btn-outline-secondary"
      v-show="props.isForm"
    >
      {{ resetText }}
    </button>

    <button :type="saveButtonType" class="btn btn-success">
      {{ saveText }}
    </button>
  </div>
</template>
