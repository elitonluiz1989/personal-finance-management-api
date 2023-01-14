import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import { BalancesRoutes } from "@/balances/balances.routes";

const routes: RouteRecordRaw[] = [
  {
    path: "/",
    redirect: () => "/balances",
  },
  ...BalancesRoutes,
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
