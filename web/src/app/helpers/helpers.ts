import { IIndexable } from "../types";

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

export function isNullOrUndefined(value: any): boolean {
  return value === undefined || value === null;
}

export function isNullOrWhiteSpace(value: string | null | undefined): boolean {
  return isNullOrUndefined(value) || value?.trim()?.length === 0;
}

export function env(key: string): string | undefined {
  return process.env[key];
}

export function vueEnv(key: string): string | undefined {
  return env(`VUE_APP_${key}`);
}
