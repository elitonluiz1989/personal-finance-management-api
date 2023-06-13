import { TransactionTypeEnum } from "../transaction-type.enum";

export class TransactionStoreDto {
  public id = 0;
  public userId = 0;
  public type: TransactionTypeEnum | undefined = undefined;
  public date: Date | undefined = undefined;
  public amount = 0;
  public installmentsIds: number[] = [];
}
