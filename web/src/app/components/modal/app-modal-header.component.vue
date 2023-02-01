<script setup lang="ts">
import { defineProps, defineEmits } from "vue";

type AppModalHeader = {
  title: string;
  labelId: string;
};
type AppModalHeaderEmits = {
  (e: "triggerOnClose"): void;
};

const props = defineProps<AppModalHeader>();

const emits = defineEmits<AppModalHeaderEmits>();

const closeModal = () => emits("triggerOnClose");
</script>

<template>
  <div class="modal-header">
    <h5 :id="props.labelId" class="modal-title">{{ props.title }}</h5>

    <button
      type="button"
      class="btn-close"
      data-dismiss="modal"
      aria-label="Close"
      @click="closeModal"
    ></button>
  </div>
</template>
