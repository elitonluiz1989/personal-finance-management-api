<script setup lang="ts">
import {
  computed,
  defineEmits,
  defineProps,
  ref,
  watch,
  withDefaults,
} from "vue";
import { AppModalFooterConfig } from "./types";

type AppModalProps = {
  id: string;
  title: string;
  show: boolean;
  footer: AppModalFooterConfig;
};
type AppModalEmits = {
  (e: "onClose", show: boolean): void;
};

const defaultFooterConfigs: AppModalFooterConfig = {
  show: false,
  saveText: "Save",
  dismissText: "Close",
};
const props = withDefaults(defineProps<AppModalProps>(), {
  show: false,
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
const footerSaveText = computed(
  () => props.footer.saveText ?? defaultFooterConfigs.saveText
);
const footerDismissText = computed(
  () => props.footer.dismissText ?? defaultFooterConfigs.dismissText
);

watch(
  () => props.show,
  (value: boolean) => {
    show.value = value;
  }
);

const closeModal = () => {
  show.value = false;

  emits("onClose", false);
};

const emits = defineEmits<AppModalEmits>();
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
        <div class="modal-header">
          <h5 :id="labelId" class="modal-title">{{ props.title }}</h5>

          <button
            type="button"
            class="close"
            data-dismiss="modal"
            aria-label="Close"
            @click="closeModal"
          >
            <span aria-hidden="true">&times;</span>
          </button>
        </div>

        <div class="modal-body">
          <slot></slot>
        </div>

        <div class="modal-footer" v-show="props.footer.show">
          <button
            type="button"
            class="btn btn-secondary"
            data-dismiss="modal"
            @click="closeModal"
          >
            {{ footerDismissText }}
          </button>

          <button type="button" class="btn btn-primary">
            {{ footerSaveText }}
          </button>
        </div>
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
