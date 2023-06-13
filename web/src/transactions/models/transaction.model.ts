import { mapFrom } from "@/app/helpers/helpers";
import { TransactionTypeEnum } from "../transaction-type.enum";
import { IIndexable } from "@/app/types";

export class Transaction {
  public id = 0;
  public userId = 0;
  public type: TransactionTypeEnum | undefined = undefined;
  public date: Date | undefined = undefined;
  public amount = 0;
  public installments: number[] = [];

  public static createFrom<TData extends IIndexable<any>>(
    data: TData
  ): Transaction {
    const transaction = new Transaction();

    mapFrom(data, transaction);

    return transaction;
  }
}
