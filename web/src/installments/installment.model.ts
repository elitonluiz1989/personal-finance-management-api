import { mapFrom } from "@/app/helpers/helpers";
import { IIndexable } from "@/app/types";
import { InstallmentStatusEnum } from "./installment-status.enum";

export class Installment {
  public id = 0;
  public balanceId = 0;
  public reference = 0;
  public referenceFormatted = "";
  public number = 0;
  public status: InstallmentStatusEnum = 0;
  public statusDescription = "";
  public amount = 0;
  public deletedAt: Date | undefined = undefined;
  public active = false;

  public static createFrom<T extends IIndexable<any>>(data: T): Installment {
    const installment = new Installment();

    mapFrom(data, installment);

    return installment;
  }
}
