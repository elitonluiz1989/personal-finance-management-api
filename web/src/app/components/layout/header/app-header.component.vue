<script setup lang="ts">
import { defineProps } from "vue";
import AppHeaderTitle from "./app-header-title.component.vue";
import { IAppNavitem } from "./types";

type AppHeaderPropsType = {
  title: string;
};
const props = defineProps<AppHeaderPropsType>();
const navItems: IAppNavitem[] = [
  {
    url: "/",
    text: "Home",
    cssClasses: "nav-link",
    active: false,
  },
  {
    url: "/balances",
    text: "Balances",
    cssClasses: "nav-link",
    active: false,
  },
];
const showNavBar = navItems.length > 1;
</script>

<template>
  <header class="container-fluid">
    <div class="row">
      <div class="header-content col-12">
        <nav class="navbar navbar-expand-lg" v-if="showNavBar">
          <div class="container-fluid p-0">
            <AppHeaderTitle :text="props.title" :url="navItems[0].url" />

            <button
              class="navbar-toggler"
              type="button"
              data-bs-toggle="collapse"
              data-bs-target="#navbarNavAltMarkup"
              aria-controls="navbarNavAltMarkup"
              aria-expanded="false"
              aria-label="Toggle navigation"
            >
              <span class="navbar-toggler-icon"></span>
            </button>

            <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
              <div class="navbar-nav">
                <template v-for="(item, index) in navItems" :key="index">
                  <router-link :class="item.cssClasses" :to="item.url">
                    {{ item.text }}
                  </router-link>
                </template>
              </div>
            </div>
          </div>
        </nav>

        <AppHeaderTitle :text="props.title" url="/" v-else />
      </div>
    </div>
  </header>
</template>

<style scoped lang="scss">
.header-content {
  padding-top: 1rem;
  padding-bottom: 0.5rem;

  .navbar-toggler {
    border: 0;

    &:focus {
      box-shadow: none;
    }

    &-icon {
      font-size: 0.8rem;
      color: #333;
    }
  }
}
</style>
