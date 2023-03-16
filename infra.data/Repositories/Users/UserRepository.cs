using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Entities;
using PersonalFinanceManagement.Domain.Users.Enums;

namespace PersonalFinanceManagement.Infra.Data.Repositories.Users
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public UserRepository(IDBContext dbContext) : base(dbContext)
        {
        }

        public UserRoleEnum? GetUserRole(int userId)
        {
            return Query()
                .Where(u => u.Id == userId)
                .Select(u => u.Role)
                .First();
        }
    }
}
