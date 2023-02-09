import { Installment } from "@/installments/installment.model";
import { BalanceTypeEnum } from "./balances-type.enum";
import { mapFrom } from "@/app/helpers/helpers";

export class Balance {
  public id = 0;
  public userId = 0;
  public name = "";
  public type: BalanceTypeEnum = BalanceTypeEnum.credit;
  public typeDescription = "";
  public date: Date = new Date();
  public amount = 0;
  public financed = false;
  public installmentsNumber = 0;
  public closed = false;
  public installments: Installment[] = [];

  public static createFrom(data: any): Balance {
    const balance = new Balance();
    const installments: Installment[] = [];

    if (data.installments?.length > 0) {
      for (const installment of data.installments) {
        installments.push(Installment.createFrom(installment));
      }

      data.installments = installments;
    }

    mapFrom(data, balance);

    return balance;
  }
}
