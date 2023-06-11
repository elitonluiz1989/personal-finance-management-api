<script setup lang="ts">
import { computed } from "@vue/reactivity";
import {
  defineEmits,
  defineProps,
  onMounted,
  ref,
  watch,
  withDefaults,
} from "vue";
import { AppComboboxLoadActionType } from "./types";

type AppFormComboboxProps = {
  loadAction: AppComboboxLoadActionType<any>;
  disabled: boolean;
  infiniteScroll: boolean;
  percentageToLoad: number;
};
type AppFormComboboxEmits = {
  (e: "onClose"): void;
  (e: "onTriggerLoadContent"): void;
};

const scrollContentRef = ref<HTMLDivElement>();
const scrollContentHeight = ref(0);
const comboboxOpened = ref(false);
const isSearching = ref(true);
const items = ref<any[]>([]);

let page = 0;

const props = withDefaults(defineProps<AppFormComboboxProps>(), {
  disabled: false,
  infiniteScroll: false,
  percentageToLoad: 100,
});
const emits = defineEmits<AppFormComboboxEmits>();

onMounted(() => {
  document.addEventListener("click", watchElementsEvent);
});

watch(scrollContentRef, (value) => {
  console.log(value?.scrollHeight);
  if (scrollContentHeight.value == 0)
    scrollContentHeight.value = value?.scrollHeight ?? 0;
});

const comboboxStyles = computed(() => ({
  "combobox form-control position-relative": props.disabled === false,
  "combobox form-control position-relative disabled": props.disabled,
}));

const watchElementsEvent = (evt: any) => {
  const elements = evt.composedPath() as HTMLElement[];

  if (!elements) return;

  const clickedOnCombobox = elements.some(
    (e) =>
      e?.classList?.contains("combobox") ||
      e?.classList?.contains("combobox__wrapper")
  );

  if (!clickedOnCombobox) {
    comboboxOpened.value = false;
    emits("onClose");
  }
};
const openCombobox = async () => {
  if (props.disabled) {
    comboboxOpened.value = false;

    return;
  }

  comboboxOpened.value = !comboboxOpened.value;

  if (comboboxOpened.value === false) return;

  isSearching.value = true;

  const result = await props.loadAction(page);

  isSearching.value = false;

  if (Array.isArray(result)) {
    items.value = result;

    return;
  }

  items.value = [];
};
const comboboxScroll = async (evt: any) => {
  if (!props.infiniteScroll || scrollContentHeight.value == 0) return;

  const scrollTop = evt.target?.scrollTop ?? 0;
  const scrollPercentage = Math.ceil(
    (scrollTop * 100) / scrollContentHeight.value
  );

  if (scrollPercentage >= props.percentageToLoad) {
    page++;
    const result = await props.loadAction(page);

    if (Array.isArray(result)) {
      items.value = result;

      return;
    }
  }
};
</script>

<template>
  <div :class="comboboxStyles" @click="openCombobox">
    <template v-if="comboboxOpened">
      <div>&nbsp;</div>

      <div
        class="combobox__wrapper w-100 overflow-hidden bg-white rounded-bottom position-absolute"
      >
        <div v-if="isSearching">Searching...</div>

        <div
          id="combobox-content"
          class="w-100 h-100 overflow-x-hidden overflow-y-scroll"
          @scroll="comboboxScroll"
          ref="scrollContentRef"
          v-else
        >
          <ul class="list-unstyled mb-0">
            <li
              class="cursor-pointer"
              v-for="(item, index) in items"
              :key="index"
            >
              <slot name="item" v-bind="item"></slot>
            </li>
          </ul>
        </div>
      </div>
    </template>

    <div :class="{ 'cursor-pointer': props.disabled === false }" v-else>
      ...
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "bootstrap/scss/functions";
@import "bootstrap/scss/variables";

.combobox {
  &__wrapper {
    height: 10rem;
    left: 0;
    background-color: $input-bg;
    border: $input-border-width solid $input-border-color;
    color: $input-color;

    ul {
      li {
        border-width: 0 0 1px;
        border-style: solid;
        border-color: $input-border-color;

        &:last-child {
          margin-bottom: 0;
          border-width: 0;
        }
      }
    }
  }
}
</style>
