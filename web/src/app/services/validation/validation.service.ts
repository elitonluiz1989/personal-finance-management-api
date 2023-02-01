import { isNullOrUndefined } from "@/app/helpers/helpers";
import { FormFields } from "../form/types";

export class ValidationService {
  private _form: FormFields;
  private _invalid = false;
  private _errors: string[] = [];

  constructor(form: FormFields) {
    this._form = form;
  }

  get isInvalid(): boolean {
    return this._invalid;
  }

  get errors(): string[] {
    return this._errors;
  }

  public validate() {
    this._errors = [];
    this._invalid = false;

    for (const field of Object.values(this._form)) {
      if (isNullOrUndefined(field.validations)) continue;

      for (const validation of field.validations) {
        if (validation.rule(field.model.value)) {
          if (field.invalid === false) field.markAsValid();

          continue;
        }

        this._errors.push(validation.message);
        field.markAsInvalid();
      }
    }

    this._invalid = this._errors.length > 0;
  }

  public clearValidation() {
    this._errors = [];
    this._invalid = false;

    for (const field of Object.values(this._form)) {
      field.markAsValid();
    }
  }
}
