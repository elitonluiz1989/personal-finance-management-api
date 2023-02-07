import { createApp } from "vue";
import "./registerServiceWorker";
import "@/app/assets/scss/app.scss";
import { AxiosHelper } from "@/app/helpers/axios";
import router from "@/app/router";
import { store, storeKey } from "@/app/store";
import App from "@/app/views/app.vue";
import { library } from "@fortawesome/fontawesome-svg-core";
import { faAngleUp as fasAngleUp } from "@fortawesome/free-solid-svg-icons";
import { faAngleDown as fasAngleDown } from "@fortawesome/free-solid-svg-icons";

library.add(fasAngleUp, fasAngleDown);

createApp(App).use(store, storeKey).use(router).mount("#app");

AxiosHelper.configure();
