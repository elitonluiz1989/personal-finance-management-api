using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Users.Dtos;
using PersonalFinanceManagement.Domain.Users.Entities;
using PersonalFinanceManagement.Domain.Users.Filters;

namespace PersonalFinanceManagement.Domain.Users.Contracts
{
    public interface IUserSpecification: ISpecification<User, int, UserFilter, UserDto>
    {
    }
}
