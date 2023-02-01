import { BalanceTypeEnum } from "./balances-type.enum";

export class Balance {
  public id = 0;
  public userId = 0;
  public name = "";
  public type: BalanceTypeEnum = BalanceTypeEnum.credit;
  public date: Date = new Date();
  public amount = 0;
  public financed = false;
  public installmentsNumber = 0;
  public closed = false;
  public installments: any[] = [];
}
