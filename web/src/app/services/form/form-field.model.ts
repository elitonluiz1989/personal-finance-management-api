import { ValidationRule } from "@/app/types";
import { Ref, ref } from "vue";
import { FormFieldOptions } from "./types";
import { isNullOrUndefined } from "@/app/helpers/helpers";

export class FormField<T> {
  public validations: ValidationRule<T>[];

  private _field: string;
  private _model: Ref<T>;
  private _invalid: Ref<boolean>;
  private _disabled: Ref<boolean>;
  private _element: HTMLElement | null = null;
  private _initialValue: T;

  constructor(field: string, options: FormFieldOptions<T>) {
    this._field = field;
    this._model = ref(options.initialValue) as Ref<T>;
    this._invalid = ref(false);
    this._disabled = ref(false);
    this.validations = options.validations;
    this._initialValue = options.initialValue;

    this.setElement();
  }

  get field(): string {
    return this._field ?? "";
  }

  get model(): Ref<T> {
    return this._model;
  }

  get invalid(): boolean {
    return this._invalid.value;
  }

  set invalid(value: boolean) {
    this._invalid.value = value;
  }

  get disabled(): boolean {
    return this._disabled.value;
  }

  get element(): HTMLElement | null {
    if (!this._element) this.setElement();

    return this._element;
  }

  get value(): T {
    return this._model.value;
  }

  get changed(): boolean {
    return (
      isNullOrUndefined(this._model.value) === false &&
      String(this._model.value) !== String(this._initialValue)
    );
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

  public enable() {
    this._disabled.value = false;
  }

  public disable() {
    this._disabled.value = true;
  }

  public clearValidation() {
    this.markAsValid();
  }

  public reset() {
    this._model.value = this._initialValue;
  }

  private setElement() {
    this._element = document.querySelector(`[form-field="${this._field}"]`);
  }

  private isMarked() {
    return this.element?.classList?.contains("is-invalid") ?? false;
  }
}
