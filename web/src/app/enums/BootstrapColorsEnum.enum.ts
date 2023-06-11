import { IIndexable } from "@/app/types";

export enum BootstrapColorsEnum {
  primary,
  secondary,
  success,
  danger,
  warning,
  info,
  light,
  dark,
}

const descriptions: IIndexable<string> = {
  [`${BootstrapColorsEnum.primary}`]: "primary",
  [`${BootstrapColorsEnum.secondary}`]: "secondary",
  [`${BootstrapColorsEnum.success}`]: "success",
  [`${BootstrapColorsEnum.danger}`]: "danger",
  [`${BootstrapColorsEnum.warning}`]: "warning",
  [`${BootstrapColorsEnum.info}`]: "info",
  [`${BootstrapColorsEnum.light}`]: "light",
  [`${BootstrapColorsEnum.dark}`]: "dark",
};

export function getBootstrapColorsEnumDescription(color: BootstrapColorsEnum) {
  return descriptions[color] ?? "";
}
