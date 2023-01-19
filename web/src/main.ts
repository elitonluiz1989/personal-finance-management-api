import { createApp } from "vue";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";
import App from "@/app/views/app.vue";
import "./registerServiceWorker";
import "@/app/assets/scss/app.scss";
import { AxiosHelper } from "@/app/helpers/axios";
import { store, storeKey } from "@/app/store";
import router from "@/app/router";

createApp(App).use(store, storeKey).use(router).mount("#app");

AxiosHelper.configure();
