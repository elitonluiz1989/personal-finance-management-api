import { authenticationGuard } from "@/authentication/authentication.guard";
import { UserRoleEnum } from "@/users/enuns/user-role.enum";
import { RouteRecordRaw } from "vue-router";
import TransactionsView from "./views/transaction-list.view.vue";

export const TransactionsRoutes: RouteRecordRaw[] = [
  {
    path: "/transactions",
    name: "transactions",
    component: TransactionsView,
    meta: {
      authorized: [UserRoleEnum.administrator],
    },
    beforeEnter: [authenticationGuard],
  },
];
