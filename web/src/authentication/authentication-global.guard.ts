import { isNullOrUndefined } from "@/app/helpers/helpers";
import { AuthenticationInfo } from "./authentication.model";
import { store, storeGet } from "@/app/store/index";
import { AuthResources } from "./authentication.resources";

export function authenticationGlobalGuard() {
  const authenticationInfo = storeGet<AuthenticationInfo>(
    AuthResources.store.getters.authentication.namespaced
  );

  if (isNullOrUndefined(authenticationInfo)) {
    store.commit(
      AuthResources.store.mutations.addAuthInfo.namespaced,
      new AuthenticationInfo()
    );
  }
}
