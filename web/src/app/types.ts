import { Ref } from "vue";

export interface IIndexable<T> {
  [key: string]: T;
}

export type ValidationRule<T> = {
  rule: (value: T) => boolean;
  message: string;
};

export type StoreResourcesItem = {
  value: string;
  namespaced: string;
};

export type AppFormField<T> = {
  id: string;
  value: Ref<T>;
  validations: ValidationRule<T>[];
  invalid: Ref<boolean>;
};

export type AppForm = {
  [key: string]: AppFormField<any>;
};

export type KeyValue<TKey, TValue> = {
  key: TKey;
  value: TValue;
};
