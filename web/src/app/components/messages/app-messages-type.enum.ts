import { IIndexable } from "@/app/types";

export enum AppMessagesTypeEnum {
  primary,
  secondary,
  sucess,
  danger,
  warning,
  info,
  light,
  alert,
}

const typesDescription: IIndexable<string> = {
  [AppMessagesTypeEnum.primary.toString()]: "primary",
  [AppMessagesTypeEnum.secondary.toString()]: "secondary",
  [AppMessagesTypeEnum.sucess.toString()]: "sucess",
  [AppMessagesTypeEnum.danger.toString()]: "danger",
  [AppMessagesTypeEnum.warning.toString()]: "warning",
  [AppMessagesTypeEnum.info.toString()]: "info",
  [AppMessagesTypeEnum.light.toString()]: "light",
  [AppMessagesTypeEnum.alert.toString()]: "alert",
};

export function getDescription(type: AppMessagesTypeEnum) {
  return typesDescription[type] ?? "";
}
