using PersonalFinanceManagement.Domain.Base.Tools.Security;
using System.Text.Json.Serialization;

namespace PersonalFinanceManagement.Application.Dtos.Authentication
{
    public class AuthenticationDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string HashedPassword => Hash.Encrypt(Password);
    }
}
