import { Ref } from "vue";

export type AppComboboxLoadActionType<T> = (page: number) => Promise<T[]>;
