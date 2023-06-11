<script setup lang="ts">
import { defineProps, ref, watch, withDefaults } from "vue";
import { AppListActionType } from "./types";

type AppFormComboboxProps = {
  search: boolean;
  action: AppListActionType<any>;
  disabled: boolean;
  infiniteScroll: boolean;
  percentageToLoad: number;
};

const scrollContentRef = ref<HTMLDivElement>();
const items = ref<any[]>([]);

let page = 0;

const props = withDefaults(defineProps<AppFormComboboxProps>(), {
  search: false,
  disabled: false,
  infiniteScroll: false,
  percentageToLoad: 100,
});

watch(
  () => props.search,
  async (value) => {
    console.log(page);
    if (value) {
      let result = await props.action(page);

      console.log(result);

      if (Array.isArray(result) === false) result = [];

      items.value = result;

      console.log(items.value);
    }
  }
);
</script>

<template>
  <div
    id="app-list"
    class="w-100 h-100 overflow-x-hidden overflow-y-scroll"
    ref="scrollContentRef"
  >
    <ul class="list-unstyled mb-0">
      <li class="cursor-pointer" v-for="(item, index) in items" :key="index">
        {{ item.id }}
        <slot name="item" v-bind="item"></slot>
      </li>
    </ul>
  </div>
</template>
