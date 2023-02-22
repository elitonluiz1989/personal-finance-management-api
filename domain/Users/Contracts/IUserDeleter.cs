using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Domain.Users.Contracts
{
    public interface IUserDeleter : IBaseDeleter<User, int>
    {
    }
}
