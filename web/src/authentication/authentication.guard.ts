import { AuthResources } from "./authentication.resources";
import { AuthenticationInfo } from "@/authentication/authentication.model";
import { AuthenticationRefreshDto } from "./types";
import router from "@/app/router";
import { isNullOrWhiteSpace } from "@/app/helpers/helpers";
import { StoreHelper } from "@/app/store/store.helper";

export async function authenticationGuard() {
  const authenticationInfo = StoreHelper.get<AuthenticationInfo | null>(
    AuthResources.store.getters.authentication.namespaced,
    null
  );

  if (authenticationInfo?.isAuthenticated === false) return redirect();

  if (authenticationInfo?.isExpired) {
    const response = prompt(AuthResources.confirmRefreshToken);

    if (isNullOrWhiteSpace(response)) return redirect();

    const dto: AuthenticationRefreshDto = {
      username: authenticationInfo?.user?.userName ?? "",
      password: response ?? "",
      token: authenticationInfo.token,
      refreshToken: authenticationInfo.refreshToken,
    };

    try {
      await StoreHelper.dispatch(
        AuthResources.store.actions.refresh.namespaced,
        dto
      );
    } catch (e) {
      router.push({ name: "login" });

      throw e;
    }
  }
}

function redirect() {
  return { name: "login" };
}
