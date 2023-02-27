import { authenticationGuard } from "@/authentication/authentication.guard";
import { UserRoleEnum } from "@/users/enuns/user-role.enum";
import { RouteRecordRaw } from "vue-router";
import HomeView from "@/home/views/home-list.view.vue";

export const HomeRoutes: RouteRecordRaw[] = [
  {
    path: "/",
    name: "home",
    component: HomeView,
    meta: {
      authorized: [UserRoleEnum.administrator],
    },
    beforeEnter: [authenticationGuard],
  },
];
