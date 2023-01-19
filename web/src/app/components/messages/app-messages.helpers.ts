import { reactive } from "vue";
import { AppMessagesTypeEnum } from "./app-messages-type.enum";
import { AppMessagesContent } from "./types";

export class AppMessagesHelper {
  private _messages = reactive<AppMessagesContent>({
    messages: [],
    type: AppMessagesTypeEnum.danger,
    show: false,
  });

  get content(): AppMessagesContent {
    return this._messages;
  }

  public reset() {
    this._messages.messages = [];
    this._messages.type = AppMessagesTypeEnum.danger;
    this._messages.show = false;
  }

  public addMessage(
    messages: string[],
    type: AppMessagesTypeEnum = AppMessagesTypeEnum.danger
  ) {
    this._messages.messages = messages;
    this._messages.type = type;
    this._messages.show = true;
  }

  public addErrors(messages: string[]) {
    this.addMessage(messages);
  }

  public addSuccess(messages: string[]) {
    this.addMessage(messages, AppMessagesTypeEnum.sucess);
  }
}
