namespace PersonalFinanceManagement.Application.Dtos.Authentication
{
    public class AuthenticationRefreshDto : AuthenticationDto
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
