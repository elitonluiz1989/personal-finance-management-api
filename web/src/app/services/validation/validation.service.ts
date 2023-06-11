import { isNullOrUndefined } from "@/app/helpers/helpers";
import { FormFields } from "../form/form-fields.model";

export class ValidationService<TFormFields extends FormFields> {
  private _form: TFormFields;
  private _invalid = false;
  private _errors: string[] = [];

  constructor(form: TFormFields) {
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

    for (const field of this._form.fieldsList) {
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
    this._form.markAllAsValid();
  }
}
