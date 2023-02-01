import { FormField } from "./form-field.model";

export type FormFieldOptions<T> = IIndexable<T> & {
  initialValue: T;
  validations: ValidationRule<T>[];
};

export type FormFieldsOptions = {
  [key: string]: FormFieldOptions<any>;
};

export type FormDataHandler = (fields: FormField) => T;

export type FormSubmitOptions = {
  action: string;
  dataHandler: FormDataHandler;
};

export type FormOptions = {
  fields: FormFieldOptions;
  submit: FormSubmitOptions;
};

export type FormFields = {
  [key: string]: FormField<any>;
};
