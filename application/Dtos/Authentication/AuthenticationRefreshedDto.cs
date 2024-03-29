﻿namespace PersonalFinanceManagement.Application.Dtos.Authentication
{
    public class AuthenticationRefreshedDto
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
    }
}
