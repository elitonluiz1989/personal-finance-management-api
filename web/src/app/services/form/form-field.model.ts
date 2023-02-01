import { ValidationRule } from "@/app/types";
import { Ref, ref } from "vue";
import { FormFieldOptions } from "./types";

export class FormField<T> {
  private _field: string;
  private _model: Ref<T>;
  private _invalid: Ref<boolean>;
  private _validations: ValidationRule<T>[];
  private _element: HTMLElement | null = null;

  constructor(field: string, options: FormFieldOptions<T>) {
    this._field = field;
    this._model = ref(options.initialValue) as Ref<T>;
    this._invalid = ref(false);
    this._validations = options.validations;

    this.setElement();
  }

  get field(): string {
    return this._field ?? "";
  }

  get model(): Ref<T> {
    return this._model;
  }

  get validations(): ValidationRule<T>[] {
    return this._validations;
  }

  get invalid(): boolean {
    return this._invalid.value;
  }

  set invalid(value: boolean) {
    this._invalid.value = value;
  }

  get element(): HTMLElement | null {
    if (!this._element) this.setElement();

    return this._element;
  }

  get value(): T {
    return this._model.value;
  }

  public markAsInvalid() {
    this._invalid.value = true;

    if (!this.element || this.isMarked()) return;

    this.element?.classList.add("is-invalid");
  }

  public markAsValid() {
    this._invalid.value = false;

    if (!this.element || this.isMarked() === false) return;

    this.element?.classList?.remove("is-invalid");
  }

  public clearValidation() {
    this.markAsValid();
  }

  private setElement() {
    this._element = document.querySelector(`[form-field="${this._field}"]`);
  }

  private isMarked() {
    return this.element?.classList?.contains("is-invalid") ?? false;
  }
}
