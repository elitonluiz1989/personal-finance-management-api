import { isBooleanAttr, isString } from "@vue/shared";
import {
  isNullOrUndefined,
  isNullOrWhiteSpace,
  isNumber,
} from "@/app/helpers/helpers";
import { ValidationRule } from "@/app/types";

export class ValidationRules {
  public static required<T>(fieldName: string): ValidationRule<T> {
    return {
      rule: (value: T) => this.requiredValidation(value),
      message: `${fieldName} is required.`,
    };
  }

  public static requiredIf<T>(fieldName: string, prerequisite: () => boolean) {
    return {
      rule: (value: T) => {
        if (prerequisite() === false) return true;

        return this.requiredValidation(value);
      },
      message: `${fieldName} is required.`,
    };
  }

  public static maxlength(
    fieldName: string,
    maxlength: number
  ): ValidationRule<string> {
    return {
      rule: (value: string) => String(value).length <= maxlength,
      message: `${fieldName} length needs to be less than ${maxlength}.`,
    };
  }

  public static between(
    fieldName: string,
    min: number,
    max: number
  ): ValidationRule<number> {
    return {
      rule: (value: number) => {
        const num = +value;

        if (isNaN(num)) return false;

        return num >= min && num <= max;
      },
      message: `${fieldName} value must be between ${min} and ${max}`,
    };
  }

  private static requiredValidation<T>(value: T) {
    if (isBooleanAttr(String(value))) return isNullOrUndefined(value) === false;

    if (isNumber(value)) {
      const numValue = Number(value);

      return isNaN(numValue) === false && numValue > 0;
    }

    if (isString(value)) return isNullOrWhiteSpace(String(value)) == false;

    return isNullOrUndefined(value) === false;
  }
}
