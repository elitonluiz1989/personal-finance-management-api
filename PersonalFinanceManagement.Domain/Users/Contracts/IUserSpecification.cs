using PersonalFinanceManagement.Domain.Users.Dtos;
using PersonalFinanceManagement.Domain.Users.Filters;

namespace PersonalFinanceManagement.Domain.Users.Contracts
{
    public interface IUserSpecification
    {
        Task<List<UserDto>> Get(UserFilter filter);
    }
}
