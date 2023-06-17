import { IAppModalFooterProps } from "@/app/components/modal/types";
import { CurrencyFormatterStatic } from "@/app/helpers/currency-formatter";
import { FormService } from "@/app/services/form/form.service";
import { ValidationService } from "@/app/services/validation/validation.service";
import { StoreHelper } from "@/app/store/store.helper";
import { TransactionFormFields } from "@/transactions/models/transaction-form-fields.model";
import { TransactionStateService as StateService } from "@/transactions/transaction-state.service";
import * as TransactionType from "@/transactions/transaction-type.enum";
import {
  TransactionsFormStrings as FormResource,
  TransactionsStoreStrings as StoreStrings,
} from "@/transactions/transactions.resources";
import { User } from "@/users/user.model";
import { ref } from "vue";

export class TransactionsFormService extends FormService<TransactionFormFields> {
  public show = ref(false);
  public modalTitle = ref(FormResource.insertTitle);
  public editMode = ref(false);
  public refinanceMode = ref(false);
  public users = ref<User[]>([]);
  public allowKeepSelectionInstalments = ref(true);
  public installmentsAmount = ref(0);
  public transactionTypes = TransactionType.toKeyValue();

  private _modalFooterConfig!: IAppModalFooterProps;
  private _page = 1;

  constructor() {
    super();

    this.disableComboxes();
  }

  get modalFooterConfig(): IAppModalFooterProps {
    return this._modalFooterConfig;
  }

  public getInstallmentsAmountFormatted(): string {
    if (!this.installmentsAmount.value)
      return CurrencyFormatterStatic.format(0);

    return CurrencyFormatterStatic.format(this.installmentsAmount.value);
  }

  public getModalTitle(): string {
    return this.editMode.value
      ? FormResource.editTitle
      : FormResource.insertTitle;
  }

  public resetState(): void {
    this.show.value = false;
    this.editMode.value = false;
    this.refinanceMode.value = false;
    this.modalTitle.value = FormResource.insertTitle;
    this.installmentsAmount.value = 0;
  }

  public validateIfAllowKeepSelection(): void {
    this.allowKeepSelectionInstalments.value =
      this.installmentsAmount.value < this._formFields.amount.value;
  }

  public async searchForUserInstallments(appendData: boolean): Promise<void> {
    this._page = appendData ? this._page + 1 : 1;

    await StateService.list(this._formFields, this._page);
  }

  public override async submit(): Promise<void> {
    this.validate();

    if (this.invalid) return;

    const dto = this._formFields.createStoreDto();

    await StoreHelper.dispatch(StoreStrings.actionAdd.namespaced, dto);
  }

  protected override initialize(): void {
    this._modalFooterConfig = { show: true };
    this._formFields = new TransactionFormFields();
    this._validateService = new ValidationService(this._formFields);
  }

  private disableComboxes() {
    this._formFields.userId.disable();
    this._formFields.type.disable();
  }
}
