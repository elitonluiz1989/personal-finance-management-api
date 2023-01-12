using PersonalFinanceManagement.Domain.Base.Tools.Security;
using System.Text.Json.Serialization;

namespace PersonalFinanceManagement.Application.Dtos.Authentication
{
    public class AuthenticationDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        [JsonIgnore]
        public string AppSecret { get; set; } = string.Empty;
        [JsonIgnore]
        public uint ExpirationDelay { get; set; }

        public string HashedPassword => Hash.Encrypt(Password);
    }
}
