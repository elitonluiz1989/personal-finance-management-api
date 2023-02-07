<script setup lang="ts">
import { computed, defineProps, ref } from "vue";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

type AppAccordionProps = {
  title: string;
};

const props = defineProps<AppAccordionProps>();
const isOpen = ref(false);
const innerContent = ref<HTMLElement>();

const bodyCssStyles = computed(() => ({
  "accordion__body accordion__body--show": isOpen.value,
  accordion__body: isOpen.value === false,
}));
const bodyHeight = computed(() => {
  if (innerContent.value?.clientHeight)
    return innerContent.value?.clientHeight + "px";

  return "fit-content";
});

const toggleAccordion = () => (isOpen.value = !isOpen.value);
</script>

<template>
  <div class="accordion">
    <button
      class="accordion__header d-flex w-100 py-2"
      @click="toggleAccordion"
    >
      <div class="flex-fill text-start fw-bolder">{{ props.title }}</div>

      <div class="accordion__icon d-flex flex-column position-relative">
        <span class="position-absolute fade" :class="{ show: isOpen }">
          <FontAwesomeIcon icon="fa-solid fa-angle-up" />
        </span>

        <span
          class="position-absolute fade"
          :class="{ show: isOpen === false }"
        >
          <FontAwesomeIcon icon="fa-solid fa-angle-down" />
        </span>
      </div>
    </button>

    <div :class="bodyCssStyles">
      <div ref="innerContent">
        <slot></slot>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
.accordion {
  &__header {
    background-color: transparent;
    border: 0;
  }

  &__icon {
    height: 24px;
    width: 14px;

    .fade {
      z-index: 1;
      top: 0;
      left: 0;
    }
  }

  &__body {
    height: 0;
    overflow: hidden;

    transition: height 0.6s ease-out;

    &--show {
      height: v-bind(bodyHeight);
    }
  }
}
</style>
