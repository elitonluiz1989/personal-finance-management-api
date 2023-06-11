import { Filter } from "@/app/models/filter.filter";
import { InstallmentStatusEnum } from "../installment-status.enum";
import { BalanceTypeEnum } from "@/balances/balances-type.enum";

export class InstallmentFilter extends Filter {
  public id = 0;
  public balanceId = 0;
  public userId = 0;
  public transactionId = 0;
  public balanceType: BalanceTypeEnum | null = null;
  public reference = 0;
  public number = 0;
  public status: InstallmentStatusEnum | null = null;
  public amount = 0;
  public appendData = false;
  public installmentToAddAtTransaction = false;
}
