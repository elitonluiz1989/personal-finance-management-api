import { createApp } from "vue";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";
import App from "./app.vue";
import "./registerServiceWorker";
import router from "@/app/router";
import store from "@/app/store";
import "@/app/assets/scss/app.scss";

createApp(App).use(store).use(router).mount("#app");
