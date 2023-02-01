import axios, { AxiosError, AxiosRequestConfig, AxiosResponse } from "axios";
import { isNullOrUndefined, vueEnv } from "@/app/helpers/helpers";
import { AuthResources } from "@/authentication/authentication.resources";
import { IIndexable } from "@/app/types";
import { StoreHelper } from "@/app/store/store.helper";

export class AxiosHelper {
  public static configure() {
    axios.defaults.baseURL = vueEnv("API_URL");
    axios.defaults.headers["Content-Type"] = "application/json";

    this.defineInterceptosGlobal();
  }

  public static defineInterceptosGlobal() {
    axios.interceptors.request.use(
      (config: AxiosRequestConfig): AxiosRequestConfig => {
        if (isNullOrUndefined(config) || isNullOrUndefined(config))
          return config;

        const token = StoreHelper.get(
          AuthResources.store.getters.token.namespaced,
          ""
        );

        (config.headers as IIndexable<string>)[
          "Authorization"
        ] = `Bearer ${token}`;

        return config;
      },
      (error: AxiosError) => this.errorHandler(error)
    );

    axios.interceptors.response.use(
      (response: AxiosResponse): AxiosResponse => response,
      (error: AxiosError) => this.errorHandler(error)
    );
  }

  private static errorHandler(error: AxiosError): Promise<AxiosError> {
    const errors = this.getErrorMessage(error);

    alert(errors);

    return Promise.reject(error);
  }

  private static getErrorMessage(error: AxiosError): string {
    const { message, response } = error;

    if (!response || !response.data) return message;

    if (Array.isArray(response.data) && response.data.length)
      return response?.data.join(", ");

    return (response.data as string)?.toString() ?? "";
  }
}
