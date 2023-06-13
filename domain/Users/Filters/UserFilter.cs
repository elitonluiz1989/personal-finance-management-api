using PersonalFinanceManagement.Domain.Base.Filters;
using PersonalFinanceManagement.Domain.Users.Enums;

namespace PersonalFinanceManagement.Domain.Users.Filters
{
    public record UserFilter : Filter
    {
        public int Id { get; set; } = default;
        public Guid Guid { get; private set; }
        public string UserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<UserRoleEnum> Roles { get; set; } = new List<UserRoleEnum>();
    }
}
