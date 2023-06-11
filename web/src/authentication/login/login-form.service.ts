import { StoreHelper } from "@/app/store/store.helper";
import { FormService } from "../../app/services/form/form.service";
import { AuthResources } from "../authentication.resources";
import { LoginFormFields } from "./login-form-fields.model";
import { ValidationService } from "@/app/services/validation/validation.service";

export class LoginFormService extends FormService<LoginFormFields> {
  public override async submit(): Promise<void> {
    const dto = this._formFields.createAuthenticationDto();

    await StoreHelper.dispatch(
      AuthResources.store.actions.login.namespaced,
      dto
    );
  }

  protected override initialize(): void {
    this._formFields = new LoginFormFields();
    this._validateService = new ValidationService(this._formFields);
  }
}
