import { FormFields } from "./form-fields.model";

export type FormFieldOptions<T> = IIndexable<T> & {
  initialValue: T;
  validations: ValidationRule<T>[];
};

export type FormDataHandler<TFormFields extends FormFields> = (
  fields: TFormFields
) => T;

export type FormSubmitHandler<TFormFields extends FormFields> = {
  action: string;
  dataHandler: FormDataHandler<TFormFields>;
};
