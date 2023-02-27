import { IAppModalFooterProps } from "@/app/components/modal/types";
import { FormService } from "@/app/services/form/form.service";
import { FormFieldsOptions, FormOptions } from "@/app/services/form/types";
import { ValidationRules } from "@/app/services/validation/validation-rules";
import { StoreHelper } from "@/app/store/store.helper";
import { KeyValue } from "@/app/types";
import { TransactionsResoures } from "@/transactions/transactions.resources";
import * as TransactionType from "@/transactions/transaction-type.enum";
import { User } from "@/users/user.model";
import { UsersResoures } from "@/users/users.resources";
import { Transaction } from "@/transactions/models/transaction.model";
import { extractDateFormDateTime } from "@/app/helpers/helpers";

export class TransactionsFormService {
  private _modalId!: string;
  private _modalFooterConfig!: IAppModalFooterProps;
  private _formFieldsOptions!: FormFieldsOptions;
  private _formOptions!: FormOptions;
  private _form!: FormService;
  private _transactionTypes!:
    | KeyValue<string, TransactionType.TransactionTypeEnum>[];

  constructor() {
    this.initialize();
  }

  get modalId(): string {
    return this._modalId;
  }

  get modalFooterConfig(): IAppModalFooterProps {
    return this._modalFooterConfig;
  }

  get formFieldsOptions(): FormFieldsOptions {
    return this._formFieldsOptions;
  }

  get formOptions(): FormOptions {
    return this._formOptions;
  }

  get form(): FormService {
    return this._form;
  }

  get transactionTypes():
    | KeyValue<string, TransactionType.TransactionTypeEnum>[] {
    return this._transactionTypes;
  }

  public dataHandler(): Transaction {
    return new Transaction();
  }

  public async getUsers(): Promise<User[]> {
    await StoreHelper.dispatch(UsersResoures.store.actions.list.namespaced, {});

    return StoreHelper.get<User[]>(
      UsersResoures.store.getters.users.namespaced,
      []
    );
  }

  private initialize() {
    this._modalId = "transaction-form-modal";
    this._modalFooterConfig = {
      show: true,
    };
    this._formFieldsOptions = {
      id: {
        initialValue: 0,
      },
      date: {
        initialValue: extractDateFormDateTime(new Date()),
        validations: [
          ValidationRules.required<string>(
            TransactionsResoures.form.fields.date
          ),
        ],
      },
      amount: {
        initialValue: 0,
        validations: [
          ValidationRules.required<number>(
            TransactionsResoures.form.fields.amount
          ),
        ],
      },
      userId: {
        initialValue: 0,
        validations: [
          ValidationRules.required<number>(
            TransactionsResoures.form.fields.user
          ),
        ],
      },
      type: {
        initialValue: 0,
        validations: [
          ValidationRules.required<number>(
            TransactionsResoures.form.fields.type
          ),
        ],
      },
      intallments: {
        initialValue: [],
        validations: [
          ValidationRules.required<any[]>(
            TransactionsResoures.form.fields.installments
          ),
        ],
      },
    };
    this._formOptions = {
      fields: this._formFieldsOptions,
      submit: {
        action: TransactionsResoures.store.actions.add.namespaced,
        dataHandler: this.dataHandler,
      },
    };
    this._form = new FormService(this._formOptions);
    this._transactionTypes = TransactionType.toKeyValue();
  }
}
