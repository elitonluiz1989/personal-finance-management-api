import { Ref } from "vue";

export interface IIndexable<T> {
  [key: string]: T;
}

export type ValidationRule<T> = {
  rule: (value: T) => boolean;
  message: string;
};

export type AppFomField<T> = {
  value: Ref<T>;
  validations: ValidationRule<T>[];
  invalid: Ref<boolean>;
};

export type AppFom = {
  [key: string]: AppFomField<any>;
};
