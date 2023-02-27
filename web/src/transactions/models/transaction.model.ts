import { TransactionTypeEnum } from "../transaction-type.enum";

export class Transaction {
  public id = 0;
  public userId = 0;
  public Type: TransactionTypeEnum = 0;
  public Date: Date | undefined = undefined;
  public Amount = 0;
}
