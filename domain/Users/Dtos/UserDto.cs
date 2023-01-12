using PersonalFinanceManagement.Domain.Users.Enums;
using System.Text.Json.Serialization;

namespace PersonalFinanceManagement.Domain.Users.Dtos
{
    public record UserDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserRoleEnum Role { get; set; }
    }
}
