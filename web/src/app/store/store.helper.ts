import { Store } from "vuex";
import { State } from "./types";
import { isNullOrUndefined } from "@/app/helpers/helpers";
import { store } from "@/app/store";

export class StoreHelper {
  static get store(): Store<State> {
    return store;
  }

  public static getRaw(getter: string): any {
    return this.store?.getters[getter];
  }

  public static get<T>(getter: string, defualtValue: T): T {
    const value = this.store?.getters[getter];

    if (isNullOrUndefined(value)) return defualtValue;

    return value as T;
  }

  public static set<T>(setter: string, payload: T): void {
    this.store?.commit(setter, payload);
  }

  public static async dispatch<T>(action: string, payload?: T): Promise<void> {
    await this.store?.dispatch(action, payload);
  }
}
