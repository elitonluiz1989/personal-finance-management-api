import { AppResources } from "@/app/app.resoures";
import { IIndexable, KeyValue } from "@/app/types";

export enum BalanceTypeEnum {
  credit = 1,
  debt,
}

const descriptions: IIndexable<string> = {
  [BalanceTypeEnum.credit.toString()]: AppResources.credit,
  [BalanceTypeEnum.debt.toString()]: AppResources.debt,
};

export function toKeyValue(): KeyValue<string, BalanceTypeEnum>[] {
  const values: KeyValue<string, BalanceTypeEnum>[] = [];

  for (const key in descriptions) {
    values.push({
      key: descriptions[key],
      value: Number(key),
    });
  }

  return values;
}
