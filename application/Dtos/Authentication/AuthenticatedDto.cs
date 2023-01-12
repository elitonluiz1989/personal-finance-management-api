using PersonalFinanceManagement.Domain.Users.Dtos;

namespace PersonalFinanceManagement.Application.Dtos.Authentication
{
    public class AuthenticatedDto
    {
        public UserDto? User { get; set; }
        public string? Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
