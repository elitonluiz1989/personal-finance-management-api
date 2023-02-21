import { createApp } from "vue";
import "./registerServiceWorker";
import "@/app/assets/scss/app.scss";
import { AxiosHelper } from "@/app/helpers/axios";
import router from "@/app/router";
import { store, storeKey } from "@/app/store";
import App from "@/app/views/app.vue";
import { library } from "@fortawesome/fontawesome-svg-core";
import {
  faAngleUp as fasAngleUp,
  faAngleDown as fasAngleDown,
  faMoneyBillTransfer as fasMoneyBillTransfer,
  faTrashCan as fasTrashCan,
  faCaretUp as fasCaretUp,
  faPlus as fasPLus,
  faPen as fasPen,
} from "@fortawesome/free-solid-svg-icons";

library.add(
  fasAngleUp,
  fasAngleDown,
  fasMoneyBillTransfer,
  fasTrashCan,
  fasCaretUp,
  fasPLus,
  fasPen
);

createApp(App).use(store, storeKey).use(router).mount("#app");

AxiosHelper.configure();
