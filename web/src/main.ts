import { createApp } from "vue";
import "./registerServiceWorker";
import "@/app/assets/scss/app.scss";
import { AxiosHelper } from "@/app/helpers/axios";
import router from "@/app/router";
import { store, storeKey } from "@/app/store";
import App from "@/app/views/app.vue";

createApp(App).use(store, storeKey).use(router).mount("#app");

AxiosHelper.configure();
