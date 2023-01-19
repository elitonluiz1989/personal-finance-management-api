import { useStore } from "@/app/store/index";
import { AuthResources } from "./authentication.resources";
import { AuthenticationInfo } from "@/authentication/authentication.model";
import { AuthenticationRefreshDto } from "./types";
import router from "@/app/router";

export async function authenticationGuard() {
  const store = useStore();
  const authenticationInfo = store.getters[
    AuthResources.store.getters.authentication.namespaced
  ] as AuthenticationInfo;

  if (authenticationInfo?.isAuthenticated === false) return redirect();

  if (authenticationInfo?.isExpired) {
    const response = confirm(AuthResources.confirmRefreshToken);

    if (response == false) return redirect();

    const dto: AuthenticationRefreshDto = {
      token: authenticationInfo.token,
      refreshToken: authenticationInfo.refreshToken,
    };

    try {
      await store.dispatch(AuthResources.store.actions.refresh.namespaced, dto);
    } catch (e) {
      router.push({ name: "login" });

      throw e;
    }
  }
}

function redirect() {
  return { name: "login" };
}
