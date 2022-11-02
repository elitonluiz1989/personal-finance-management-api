using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Domain.Users.Contracts
{
    public interface IUserRepository : IRepository<User, int>
    {
    }
}
