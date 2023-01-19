import axios, { AxiosError, AxiosResponse } from "axios";
import { vueEnv } from "@/app/helpers/helpers";

export class AxiosHelper {
  public static configure() {
    axios.defaults.baseURL = vueEnv("API_URL");
    axios.defaults.headers["Content-Type"] = "application/json";

    this.defineInterceptosGlobal();
  }

  public static defineInterceptosGlobal() {
    axios.interceptors.response.use(
      (response: AxiosResponse): AxiosResponse => response,
      (error: AxiosError): Promise<AxiosError> => {
        const errors = (error.response?.data ?? [error.message]) as string[];

        alert(errors.join(", "));

        return Promise.reject(error);
      }
    );
  }
}
