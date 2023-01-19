export type AuthenticationDto = {
  username: string;
  password: string;
};

export type AuthenticationRefreshDto = {
  token: string;
  refreshToken: string;
  expires?: Date;
};
