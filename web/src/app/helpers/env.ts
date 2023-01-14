export class Env {
  public static get(key: string): string {
    return process.env[key] ?? "";
  }
}
