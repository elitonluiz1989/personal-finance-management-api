import { UserRoleEnum } from "@/users/enuns/user-role.enum";

export class UserAuthenticated {
  public id = 0;
  public userName = "";
  public name = "";
  public email = "";
  public role: UserRoleEnum | undefined;
}
