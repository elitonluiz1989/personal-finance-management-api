import { StoreHelper } from "@/app/store/store.helper";
import { Installment } from "@/installments/installment.model";
import { InstallmentsResources } from "@/installments/installments.resources";
import { InstallmentFilter } from "@/installments/models/installment.filter";
import { User } from "@/users/user.model";
import { UsersResoures } from "@/users/users.resources";
import { TransactionFormFields } from "./models/transaction-form-fields.model";

export class TransactionStateService {
  public static async getUsers(): Promise<User[]> {
    await StoreHelper.dispatch(UsersResoures.store.actions.list.namespaced, {});

    return StoreHelper.get<User[]>(
      UsersResoures.store.getters.users.namespaced,
      []
    );
  }

  public static async list(
    fields: TransactionFormFields,
    page: number
  ): Promise<void> {
    const filter = new InstallmentFilter();
    filter.userId = fields.userId.value;
    filter.balanceType = fields.type.value;
    filter.page = page;
    filter.appendData = true;
    filter.withoutPagination = false;
    filter.installmentToAddAtTransaction = true;

    await StoreHelper.dispatch(
      InstallmentsResources.store.actions.list.namespaced,
      filter
    );
  }

  public static clearInstallments() {
    StoreHelper.set<Installment[]>(
      InstallmentsResources.store.mutations.addInstallments.namespaced,
      []
    );
  }
}
