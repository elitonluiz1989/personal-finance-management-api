import { IIndexable } from "@/app/types";

export interface AppModalFooterConfig
  extends IIndexable<boolean | string | null> {
  show: boolean;
  saveText?: string;
  dismissText?: string;
}
