import { IAppModalFooterProps } from "@/app/components/modal/types";
import { FormService } from "@/app/services/form/form.service";
import { ValidationService } from "@/app/services/validation/validation.service";
import { StoreHelper } from "@/app/store/store.helper";
import { KeyValue } from "@/app/types";
import { BalanceTypeEnum } from "@/balances/balances-type.enum";
import { Installment } from "@/installments/installment.model";
import { InstallmentsResources } from "@/installments/installments.resources";
import { InstallmentFilter } from "@/installments/models/installment.filter";
import { TransactionFormFields } from "@/transactions/models/transaction-form-fields.model";
import * as TransactionType from "@/transactions/transaction-type.enum";
import { TransactionsResources } from "@/transactions/transactions.resources";
import { User } from "@/users/user.model";
import { UsersResoures } from "@/users/users.resources";

export class TransactionsFormService extends FormService<TransactionFormFields> {
  private _modalFooterConfig!: IAppModalFooterProps;
  private _transactionTypes: KeyValue<
    string,
    TransactionType.TransactionTypeEnum
  >[] = [];
  private _page = 1;

  constructor() {
    super();

    this.disableComboxes();
  }

  get modalFooterConfig(): IAppModalFooterProps {
    return this._modalFooterConfig;
  }

  get transactionTypes(): KeyValue<
    string,
    TransactionType.TransactionTypeEnum
  >[] {
    return this._transactionTypes;
  }

  public async getUsers(): Promise<User[]> {
    await StoreHelper.dispatch(UsersResoures.store.actions.list.namespaced, {});

    return StoreHelper.get<User[]>(
      UsersResoures.store.getters.users.namespaced,
      []
    );
  }

  public async searchForUserInstallments(
    userId: number,
    type: BalanceTypeEnum,
    appendData: boolean
  ): Promise<void> {
    this._page = appendData ? this._page + 1 : 1;

    const filter = new InstallmentFilter();
    filter.userId = userId;
    filter.balanceType = type;
    filter.page = this._page;
    filter.appendData = true;
    filter.withoutPagination = false;
    filter.installmentToAddAtTransaction = true;

    await StoreHelper.dispatch(
      InstallmentsResources.store.actions.list.namespaced,
      filter
    );
  }

  public clearStatedInstallments() {
    StoreHelper.set<Installment[]>(
      InstallmentsResources.store.mutations.addInstallments.namespaced,
      []
    );
  }

  public override async submit(): Promise<void> {
    const dto = this._formFields.createTransaction();

    await StoreHelper.dispatch(
      TransactionsResources.store.actions.add.namespaced,
      dto
    );
  }

  protected override initialize(): void {
    this._modalFooterConfig = { show: true };
    this._transactionTypes = TransactionType.toKeyValue();
    this._formFields = new TransactionFormFields();
    this._validateService = new ValidationService(this._formFields);
  }

  private disableComboxes() {
    this._formFields.userId.disable();
    this._formFields.type.disable();
  }
}
