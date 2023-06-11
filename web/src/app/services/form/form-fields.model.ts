import { ValidationRule } from "@/app/types";
import { ValidationRules } from "../validation/validation-rules";
import { FormField } from "./form-field.model";

export abstract class FormFields {
  protected _fieldsList: FormField<any>[] = [];

  public get fieldsList(): FormField<any>[] {
    return this._fieldsList;
  }

  public enableAll() {
    for (const field of this._fieldsList) {
      if (field.disabled === false) continue;

      field.enable();
    }
  }

  public disableAll() {
    for (const field of this._fieldsList) {
      if (field.disabled) continue;

      field.disable();
    }
  }

  public resetAll() {
    for (const field of this._fieldsList) {
      field.reset();
    }
  }

  public markAllAsValid() {
    for (const field of this._fieldsList) {
      field.markAsValid();
    }
  }

  protected createFormField<T>(
    name: string,
    initialValue: T,
    validations: ValidationRule<T>[] = []
  ): FormField<T> {
    const formField = new FormField<T>(name, { initialValue });

    if (Array.isArray(validations)) formField.validations = validations;

    return formField;
  }

  protected createFormFieldRequired<T>(
    name: string,
    initialValue: T
  ): FormField<T> {
    return this.createFormField(name, initialValue, [
      ValidationRules.required<T>(name),
    ]);
  }

  protected fillFieldsList(): void {
    const fields = Object.values(this);

    for (const field of fields) {
      if (field instanceof FormField) {
        this._fieldsList.push(field);
      }
    }
  }
}
