import { FormField } from "@/app/services/form/form-field.model";
import { FormFields } from "@/app/services/form/form-fields.model";
import { AuthResources } from "../authentication.resources";
import { AuthenticationDto } from "../types";

export class LoginFormFields extends FormFields {
  public username: FormField<string>;
  public password: FormField<string>;

  constructor() {
    super();

    this.username = this.createFormFieldRequired(
      AuthResources.form.username,
      ""
    );
    this.password = this.createFormFieldRequired(
      AuthResources.form.password,
      ""
    );
  }

  public createAuthenticationDto(): AuthenticationDto {
    return {
      username: this.username.model.value,
      password: this.password.model.value,
    };
  }
}
