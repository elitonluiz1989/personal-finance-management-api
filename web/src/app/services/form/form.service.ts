import { StoreHelper } from "@/app/store/store.helper";
import { ValidationService } from "../validation/validation.service";
import { FormField } from "./form-field.model";
import { FormFields, FormOptions } from "./types";

export class FormService {
  private _fields: FormFields = {};
  private _validateService: ValidationService;

  constructor(private _options: FormOptions) {
    this.createFormFields();

    this._validateService = new ValidationService(this._fields);
  }

  get invalid(): boolean {
    return this._validateService.isInvalid;
  }

  get fields(): FormFields {
    return this._fields;
  }

  public validate() {
    this._validateService.validate();
  }

  public clearValidations() {
    this._validateService.clearValidation();
  }

  public reset() {
    this.clearValidations();

    this.resetValues();
  }

  public async submit() {
    const dto = this._options.submit.dataHandler(this._fields);

    await StoreHelper.dispatch(this._options.submit.action, dto);
  }

  private createFormFields() {
    for (const key in this._options.fields) {
      this._fields[key] = new FormField(key, this._options.fields[key]);
    }
  }

  private resetValues() {
    for (const key in this._options.fields) {
      this._fields[key].model.value = this._options.fields[key].initialValue;
    }
  }
}
