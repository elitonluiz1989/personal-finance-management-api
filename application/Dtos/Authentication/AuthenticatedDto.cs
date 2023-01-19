using PersonalFinanceManagement.Domain.Users.Dtos;

namespace PersonalFinanceManagement.Application.Dtos.Authentication
{
    public class AuthenticatedDto
    {
        public UserDto? User { get; set; }
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
    }
}
