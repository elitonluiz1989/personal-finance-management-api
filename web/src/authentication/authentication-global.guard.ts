import { isNullOrUndefined } from "@/app/helpers/helpers";
import { StoreHelper } from "@/app/store/store.helper";
import { AuthenticationInfo } from "./authentication.model";
import { AuthResources } from "./authentication.resources";

export function authenticationGlobalGuard() {
  const authenticationInfo = StoreHelper.get<AuthenticationInfo | null>(
    AuthResources.store.getters.authentication.namespaced,
    null
  );

  if (isNullOrUndefined(authenticationInfo)) {
    StoreHelper.set(
      AuthResources.store.mutations.addAuthInfo.namespaced,
      new AuthenticationInfo()
    );
  }
}
