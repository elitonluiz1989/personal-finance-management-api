namespace PersonalFinanceManagement.Application.Dtos.Authentication
{
    public class AuthenticationRefreshDto
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
