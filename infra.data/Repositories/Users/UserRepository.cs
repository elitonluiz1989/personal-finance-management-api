using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Infra.Data.Repositories.Users
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public UserRepository(IDBContext dbContext) : base(dbContext)
        {
        }
    }
}
