import { Ref } from "vue";

export type AppListActionType<T> = (page: number) => Promise<T[]>;
