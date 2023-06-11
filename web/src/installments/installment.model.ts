import { mapFrom } from "@/app/helpers/helpers";
import { IIndexable } from "@/app/types";
import { InstallmentStatusEnum } from "./installment-status.enum";
import { BalanceSimplified } from "./../balances/models/balance-simplified.model";

export class Installment {
  public id = 0;
  public balanceId = 0;
  public reference = 0;
  public referenceFormatted = "";
  public number = 0;
  public status: InstallmentStatusEnum = InstallmentStatusEnum.created;
  public statusDescription = "";
  public amount = 0;
  public deletedAt: Date | undefined = undefined;
  public active = false;
  public balance: BalanceSimplified | undefined = undefined;

  public static castList<T extends IIndexable<any>>(data: T[]): Installment[] {
    const installments: Installment[] = [];

    for (const installment of data) {
      installments.push(this.createFrom(installment));
    }

    return installments;
  }

  public static createFrom<T extends IIndexable<any>>(data: T): Installment {
    const installment = new Installment();

    if (data.balance) {
      installment.balance = BalanceSimplified.createFrom(data.balance);

      delete data.balance;
    }

    mapFrom(data, installment);

    return installment;
  }
}
