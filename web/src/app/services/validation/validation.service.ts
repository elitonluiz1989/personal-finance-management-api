import { isNullOrUndefined } from "@/app/helpers/helpers";
import { AppFom } from "@/app/types";

export class ValidationService {
  private _invalid = false;
  private _errors: string[] = [];

  get isInvalid(): boolean {
    return this._invalid;
  }

  get errors(): string[] {
    return this._errors;
  }

  public validate(form: AppFom) {
    this._errors = [];
    this._invalid = false;

    for (const field of Object.values(form)) {
      if (isNullOrUndefined(field.validations)) continue;

      for (const validation of field.validations) {
        if (validation.rule(field.value.value) === false) {
          this._errors.push(validation.message);
          field.invalid.value = true;
        }
      }
    }

    this._invalid = this._errors.length > 0;
  }
}
