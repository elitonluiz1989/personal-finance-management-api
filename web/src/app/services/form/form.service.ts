import { ValidationService } from "../validation/validation.service";
import { FormSubmitHandler } from "./types";
import { FormFields } from "./form-fields.model";

export abstract class FormService<TFormFields extends FormFields> {
  protected _formFields!: TFormFields;
  protected _submitHandler!: FormSubmitHandler<TFormFields>;
  protected _validateService!: ValidationService<TFormFields>;

  constructor() {
    this.initialize();
  }

  public get invalid(): boolean {
    return this._validateService.isInvalid;
  }

  public get fields(): TFormFields {
    return this._formFields;
  }

  public validate(): void {
    this._validateService.validate();
  }

  public clearValidations(): void {
    this._validateService.clearValidation();
  }

  public reset(): void {
    this.clearValidations();
    this._formFields.resetAll();
  }

  public enableAll(): void {
    this._formFields.enableAll();
  }

  public disableAll(): void {
    this._formFields.disableAll();
  }

  public abstract submit(): Promise<void>;

  protected abstract initialize(): void;
}
