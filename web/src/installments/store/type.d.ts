import { PaginationStateType } from "@/app/types";
import { Installment } from "../installment.model";

export type InstallmentsState = {
  installments: Installment[];
  pagination: PaginationStateType;
};
