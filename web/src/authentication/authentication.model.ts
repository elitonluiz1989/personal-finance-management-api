import { isNullOrWhiteSpace, mapTo } from "@/app/helpers/helpers";
import { UserAuthenticated } from "@/users/user-authenticated.model";

export class AuthenticationInfo {
  public user: UserAuthenticated | undefined;
  public token = "";
  public refreshToken = "";
  public expires: Date | undefined;

  get isAuthenticated(): boolean {
    return isNullOrWhiteSpace(this.token) === false;
  }

  get isExpired(): boolean {
    if (this.expires == undefined || this.expires === null) return true;

    return this.expires.getTime() < Date.now();
  }

  public static deserialize(json: string): AuthenticationInfo {
    const obj = JSON.parse(json, (key: string, value: any) => {
      if (key.toLocaleLowerCase() === "expires")
        return new Date(value.toString());

      if (key.toLocaleLowerCase() === "user") {
        const user = new UserAuthenticated();

        mapTo(value, user);

        return user;
      }

      return value;
    });

    const newInstance = new AuthenticationInfo();

    mapTo(obj, newInstance);

    return newInstance;
  }
}
