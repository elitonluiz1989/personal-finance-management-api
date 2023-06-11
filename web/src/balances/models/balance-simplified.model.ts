import { mapFrom } from "@/app/helpers/helpers";
import { BalanceTypeEnum } from "../balances-type.enum";

export class BalanceSimplified {
  public id = 0;
  public userId = 0;
  public name = "";
  public type: BalanceTypeEnum | null = null;
  public typeDescription = "";
  public installmentsNumber = 0;

  public static createFrom(data: any): BalanceSimplified {
    const balanceSimplified = new BalanceSimplified();

    mapFrom(data, balanceSimplified);

    return balanceSimplified;
  }
}
