import { Installment } from "@/installments/installment.model";
import { TransactionStateService as StateService } from "@/transactions/transaction-state.service";
import { TransactionsFormService } from "./transactions-form.service";

export class TransactionsFormEventsService {
  constructor(private _form: TransactionsFormService) {}

  public amountChangeHandler(): void {
    if (this._form.fields.amount.changed) {
      this._form.fields.userId.enable();
      this._form.fields.type.enable();

      this._form.validateIfAllowKeepSelection();

      return;
    }

    this._form.fields.userId.disable();
    this._form.fields.type.disable();
  }

  public async userChangeHandler(): Promise<void> {
    this._form.fields.userId.clearValidation();

    await this.searchInstallments();
  }

  public async typeChangeHandler(): Promise<void> {
    this._form.fields.type.clearValidation();

    await this.searchInstallments();
  }

  public selectedInstallmentHandler(installment: Installment): void {
    const alreadySelected = this._form.fields.installments.value.find(
      (item) => item.id === installment.id
    );

    if (alreadySelected) {
      this._form.fields.installments.model.value =
        this._form.fields.installments.model.value.filter(
          (item) => item.id !== installment.id
        );
    } else {
      this._form.fields.installments.model.value.push(installment);
    }

    this._form.installmentsAmount.value =
      this._form.fields.installments.model.value.reduce(
        (acc, installment) => acc + installment.amount,
        0
      );

    this._form.validateIfAllowKeepSelection();
  }

  public closeModalHandler(): void {
    this._form.resetState();
    this._form.enableAll();
    this._form.reset();
    StateService.clearInstallments();
  }

  public async populateUsers(): Promise<void> {
    this._form.users.value = await StateService.getUsers();
  }

  public async searchInstallments(appendData = false): Promise<void> {
    if (
      this._form.fields.userId.changed === false ||
      this._form.fields.type.changed === false
    ) {
      StateService.clearInstallments();

      return;
    }

    if (appendData === false) StateService.clearInstallments();

    await this._form.searchForUserInstallments(appendData);
  }

  public reset(): void {
    this._form.reset();
    this._form.installmentsAmount.value = 0;
    StateService.clearInstallments();
  }
}
