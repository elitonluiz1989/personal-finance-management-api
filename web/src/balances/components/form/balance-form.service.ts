import { FormService } from "@/app/services/form/form.service";
import { ValidationService } from "@/app/services/validation/validation.service";
import { StoreHelper } from "@/app/store/store.helper";
import { BalancesResources } from "@/balances/balances.resources";
import { BalanceFormFields } from "@/balances/models/balance-form-fields.model";
import { User } from "@/users/user.model";
import { UsersResoures } from "@/users/users.resources";
import { ref } from "vue";

export class BalanceFormService extends FormService<BalanceFormFields> {
  public editMode = ref(false);
  public refinanceMode = ref(false);

  public resetModes(): void {
    this.editMode.value = false;
    this.refinanceMode.value = false;
  }

  public enableEditMode(balanceId: number, refinancing = false): void {
    this.editMode.value = true;
    this.refinanceMode.value = refinancing;

    const balance = StoreHelper.getRaw(
      BalancesResources.store.getters.balance.namespaced
    )(balanceId);

    this.fields.fill(balance);
    this.fields.disableByMode(this.refinanceMode.value);
  }

  public async getUsers(): Promise<User[]> {
    await StoreHelper.dispatch(UsersResoures.store.actions.list.namespaced, {});

    return StoreHelper.get<User[]>(
      UsersResoures.store.getters.users.namespaced,
      []
    );
  }

  public verifyIfShowFinancedValue(): boolean {
    if (
      isNaN(this.fields.amount.value) ||
      isNaN(this.fields.installmentsNumber.value)
    )
      return false;

    return this.fields.amount.changed && this.fields.installmentsNumber.changed;
  }

  public financedValueFormatted(): string {
    if (this.verifyIfShowFinancedValue() === false) return "0";

    const financedValue =
      this.fields.amount.value / this.fields.installmentsNumber.value;

    return financedValue.toFixed(2);
  }

  public override async submit(): Promise<void> {
    this.validate();

    if (this.invalid) return;

    if (this.refinanceMode.value) {
      await this.refinance();

      return;
    }

    await this.create();
  }

  protected override initialize(): void {
    this._formFields = new BalanceFormFields();
    this._validateService = new ValidationService(this._formFields);
  }

  private async refinance(): Promise<void> {
    const dto = this._formFields.createRefinancedBalance();

    await StoreHelper.dispatch(
      BalancesResources.store.actions.refinance.namespaced,
      dto
    );
  }

  private async create(): Promise<void> {
    const dto = this._formFields.createBalance();

    await StoreHelper.dispatch(
      BalancesResources.store.actions.add.namespaced,
      dto
    );
  }
}
