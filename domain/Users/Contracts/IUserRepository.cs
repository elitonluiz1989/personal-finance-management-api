using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Users.Entities;
using PersonalFinanceManagement.Domain.Users.Enums;

namespace PersonalFinanceManagement.Domain.Users.Contracts
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<UserRoleEnum?> GetUserRole(int userId);
    }
}
