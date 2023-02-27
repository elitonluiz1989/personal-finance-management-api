import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import { BalancesRoutes } from "@/balances/balances.routes";
import { AuthenticationRoutes } from "@/authentication/authentication.routes";
import { authenticationGlobalGuard } from "@/authentication/authentication-global.guard";
import { HomeRoutes } from "@/home/home.routes";

const routes: RouteRecordRaw[] = [
  ...HomeRoutes,
  ...AuthenticationRoutes,
  ...BalancesRoutes,
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

router.beforeEach(authenticationGlobalGuard);

export default router;
