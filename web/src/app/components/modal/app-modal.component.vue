<script setup lang="ts">
import {
  computed,
  defineEmits,
  defineProps,
  ref,
  watch,
  withDefaults,
} from "vue";
import { IAppModalFooterProps } from "./types";
import AppModalHeader from "./app-modal-header.component.vue";
import AppModalFooter from "./app-modal-footer.compenent.vue";

type AppModalProps = {
  id: string;
  title: string;
  show: boolean;
  isForm: boolean;
  footer: IAppModalFooterProps;
};
type AppModalEmits = {
  (e: "onClose", show: boolean): void;
  (e: "onReset", evt: Event): void;
  (e: "onSave", evt: Event): void;
};

const props = withDefaults(defineProps<AppModalProps>(), {
  show: false,
  isForm: false,
});
const show = ref(false);
const labelId = `${props.id}-label`;

const modalCssStyles = computed(() => ({
  "modal fade show": show.value,
  "modal fade": show.value === false,
}));
const modalDropCssStyles = computed(() => ({
  "modal-backdrop fade show": show.value,
  "modal-backdrop fade": show.value === false,
}));

watch(
  () => props.show,
  (value: boolean) => {
    show.value = value;
  }
);

const emits = defineEmits<AppModalEmits>();

const closeModal = () => {
  show.value = false;

  emits("onClose", false);
};
const submitForm = (evt: Event) => emits("onSave", evt);
const resetForm = (evt: Event) => emits("onReset", evt);
</script>

<template>
  <div
    :id="props.id"
    :class="modalCssStyles"
    tabindex="-1"
    role="dialog"
    :aria-labelledby="labelId"
    aria-hidden="true"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <form v-if="props.isForm" @submit="submitForm" @reset="resetForm">
          <AppModalHeader
            :title="props.title"
            :label-id="labelId"
            @trigger-on-close="closeModal"
          />

          <div class="modal-body">
            <slot></slot>
          </div>

          <AppModalFooter
            :is-form="true"
            :save-text="props.footer.saveText"
            :reset-text="props.footer.dismissText"
            :dismiss-text="props.footer.dismissText"
            @trigger-on-close="closeModal"
          />
        </form>

        <template v-else>
          <AppModalHeader
            :title="props.title"
            :label-id="labelId"
            @trigger-on-close="closeModal"
          />

          <div class="modal-body">
            <slot></slot>
          </div>

          <AppModalFooter
            :is-form="false"
            :save-text="props.footer.saveText"
            :reset-text="props.footer.dismissText"
            :dismiss-text="props.footer.dismissText"
            @trigger-on-close="closeModal"
          />
        </template>
      </div>
    </div>
  </div>

  <div :class="modalDropCssStyles"></div>
</template>

<style scoped lang="scss">
$modal-zindex: 1071;

%modal-inital {
  z-index: -1;
}

.close {
  background-color: transparent;
  border: 0;
}

.modal {
  display: block;

  @extend %modal-inital;

  &.show {
    z-index: $modal-zindex + 1;

    .modal-dialog {
      opacity: 1;
    }
  }

  &-dialog {
    opacity: 0;
  }
}

.modal-backdrop {
  @extend %modal-inital;

  &.show {
    z-index: $modal-zindex;
  }
}
</style>
