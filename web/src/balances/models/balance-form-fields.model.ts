import { extractDateFormDateTime } from "@/app/helpers/helpers";
import { FormField } from "@/app/services/form/form-field.model";
import { FormFields } from "@/app/services/form/form-fields.model";
import { ValidationRules as Rules } from "@/app/services/validation/validation-rules";
import { RefinancedBalance } from "./balance-refinanced.model";
import { BalancesResources } from "../balances.resources";
import { Balance } from "./balance.model";

export class BalanceFormFields extends FormFields {
  public id: FormField<number>;
  public userId: FormField<number>;
  public name: FormField<string>;
  public type: FormField<number>;
  public date: FormField<string>;
  public amount: FormField<number>;
  public financed: FormField<boolean>;
  public installmentsNumber: FormField<number>;

  constructor() {
    super();

    this.id = this.createFormField(BalancesResources.form.fields.id, 0);
    this.userId = this.createFormFieldRequired(
      BalancesResources.form.fields.user,
      0
    );
    this.name = this.createFormField<string>(
      BalancesResources.form.fields.name,
      "",
      [
        Rules.required<string>(BalancesResources.form.fields.name),
        Rules.maxlength(BalancesResources.form.fields.name, 100),
      ]
    );
    this.type = this.createFormFieldRequired<number>(
      BalancesResources.form.fields.type,
      0
    );
    this.date = this.createFormFieldRequired<string>(
      BalancesResources.form.fields.date,
      extractDateFormDateTime(new Date())
    );
    this.amount = this.createFormFieldRequired(
      BalancesResources.form.fields.amount,
      0
    );
    this.financed = this.createFormFieldRequired<boolean>(
      BalancesResources.form.fields.financed,
      false
    );
    this.installmentsNumber = this.createFormField<number>(
      BalancesResources.form.fields.intallmentsNumber,
      0,
      [
        Rules.requiredIf<number>(
          BalancesResources.form.fields.intallmentsNumber,
          () => this.financed.value
        ),
        Rules.between(BalancesResources.form.fields.intallmentsNumber, 1, 999),
      ]
    );

    this.fillFieldsList();
  }

  public fill(balance: Balance): void {
    if (!balance) return;

    this.id.model.value = balance.id;
    this.userId.model.value = balance.userId;
    this.name.model.value = balance.name;
    this.type.model.value = balance.type ?? 0;
    this.date.model.value = extractDateFormDateTime(balance.date as Date);
    this.amount.model.value = balance.amount;
    this.financed.model.value = balance.financed;
    this.installmentsNumber.model.value = balance.installmentsNumber;
  }

  public disableByMode(refinanceMode: boolean): void {
    if (refinanceMode) {
      this.name.disable();
      this.userId.disable();
      this.type.disable();

      return;
    }

    this.userId.disable();
    this.amount.disable();
    this.date.disable();
    this.financed.disable();
    this.installmentsNumber.disable();
  }

  public createBalance(): Balance {
    const balance = new Balance();

    if (this.id.value > 0) balance.id = this.id.value;

    balance.userId = this.userId.value;
    balance.name = this.name.value;
    balance.type = this.type.value;
    balance.date = new Date(this.date.value);
    balance.amount = this.amount.value;
    balance.financed = this.financed.value;

    if (balance.financed)
      balance.installmentsNumber = this.installmentsNumber.value;

    return balance;
  }

  public createRefinancedBalance(): RefinancedBalance {
    const refinancedBalance = new RefinancedBalance();
    refinancedBalance.balanceId = this.id.value;
    refinancedBalance.name = this.name.value;
    refinancedBalance.date = new Date(this.date.value);
    refinancedBalance.amount = this.amount.value;
    refinancedBalance.financed = this.financed.value;
    refinancedBalance.installmentsNumber = this.installmentsNumber.value;

    return refinancedBalance;
  }
}
