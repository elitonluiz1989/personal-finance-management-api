import { isNullOrWhiteSpace } from "@/app/helpers/helpers";
import { ValidationRule } from "@/app/types";

export class ValidationRules {
  public static required(fieldName: string): ValidationRule<string> {
    return {
      rule: (value: string) => isNullOrWhiteSpace(value.toString()) == false,
      message: `${fieldName} is required.`,
    };
  }
}
