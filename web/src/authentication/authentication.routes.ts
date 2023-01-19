import { RouteRecordRaw } from "vue-router";
import LoginView from "./login/login-form.view.vue";

export const AuthenticationRoutes: RouteRecordRaw[] = [
  {
    path: "/login",
    name: "login",
    component: LoginView,
  },
];
