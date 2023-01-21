export type AuthenticationDto = {
  username: string;
  password: string;
};

export type AuthenticationRefreshDto = {
  username: string;
  password: string;
  token: string;
  refreshToken: string;
  expires?: Date;
};
