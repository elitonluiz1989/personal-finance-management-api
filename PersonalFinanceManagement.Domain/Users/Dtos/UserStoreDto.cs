using PersonalFinanceManagement.Domain.Base.Dtos;
using PersonalFinanceManagement.Domain.Base.Tools.Security;
using PersonalFinanceManagement.Domain.Users.Enums;
using System.Text.Json.Serialization;

namespace PersonalFinanceManagement.Domain.Users.Dtos
{
    public record UserStoreDto : RecordedDto<int>
    {
        public string UserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRoleEnum Role { get; set; }

        public string HashedPassword => Hash.Encrypt(Password);
    }
}
