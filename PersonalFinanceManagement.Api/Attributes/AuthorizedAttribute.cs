using Microsoft.AspNetCore.Authorization;
using PersonalFinanceManagement.Domain.Users.Enums;

namespace PersonalFinanceManagement.Api.Attributes
{
    public class AuthorizedAttribute : AuthorizeAttribute
    {
        public AuthorizedAttribute(params UserRoleEnum[] roles) : base()
        {
            var rolesValues = roles.Select(r => r.GetHashCode()).ToArray();
            Roles = string.Join(",", rolesValues);
        }
    }
}
