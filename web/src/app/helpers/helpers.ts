import { isArray } from "@vue/shared";
import { IIndexable, StoreResourcesItem } from "../types";

function hasKey<T extends object>(obj: T, key: PropertyKey): key is keyof T {
  return key in obj;
}

export function mapTo<
  TSource extends IIndexable<any>,
  TTarget extends IIndexable<any>
>(from: TSource, to: TTarget) {
  if (isNullOrUndefined(from) || isNullOrUndefined(to)) return;

  for (const key in from) {
    if (hasKey(to, key) === false) return;

    to[key] = from[key] as any;
  }
}

export function mapFrom<
  TSource extends IIndexable<any>,
  TTarget extends IIndexable<any>
>(source: TSource, target: TTarget) {
  if (isNullOrUndefined(source) || isNullOrUndefined(target)) return;

  for (const key in source) {
    if (hasKey(target, key) === false) continue;

    if (isArray(target[key])) {
      (target[key] as any) = source[key];

      continue;
    }

    if (isDate(target[key])) {
      (target[key] as Date) = new Date(String(source[key]));

      continue;
    }

    if (isBoolean(target[key])) {
      (target[key] as boolean) = source[key] as boolean;

      continue;
    }

    if (isNumber(target[key])) {
      (target[key] as number) = Number(String(source[key]));

      continue;
    }

    (target[key] as any) = source[key];
  }
}

export function isNullOrUndefined(value: any): boolean {
  return value === undefined || value === null;
}

export function isNullOrWhiteSpace(value: string | null | undefined): boolean {
  return isNullOrUndefined(value) || value?.trim()?.length === 0;
}

export function env(key: string, defaultValue = ""): string {
  return process.env[key] ?? defaultValue;
}

export function vueEnv(key: string, defaultValue = ""): string {
  return env(`VUE_APP_${key}`, defaultValue);
}

export function createStoreResourceItem(
  namespace: string,
  value: string
): StoreResourcesItem {
  return {
    value: value,
    namespaced: `${namespace}/${value}`,
  };
}

export function isString<T>(value: T): boolean {
  return typeof value === "string";
}

export function isNumber<T>(value: T): boolean {
  return typeof value === "number" && isNaN(Number(value)) === false;
}

export function isBoolean<T>(value: T): boolean {
  return typeof value === "boolean";
}

export function isDate<T>(value: T) {
  if (value instanceof Date === false) return false;

  const date = new Date(String(value));

  return date && isValidNumber(date.getDate());
}

export function isValidNumber<T extends number>(value: T): boolean {
  return isNaN(value) === false;
}

export function getEventTarget<T extends EventTarget>(evt: Event): T {
  const { target } = evt;
  return target as T;
}
