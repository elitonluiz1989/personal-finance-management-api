import { IIndexable } from "@/app/types";

export interface IAppModalFooterProps
  extends IIndexable<boolean | string | null> {
  show: boolean;
  saveText?: string;
  resetText?: string;
  dismissText?: string;
}

export type AppModalFooterProps = IAppModalFormProps & IAppModalFooterProps;
