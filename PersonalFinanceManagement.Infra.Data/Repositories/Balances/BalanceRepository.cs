using PersonalFinanceManagement.Domain.Balances.Contracts;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Infra.Data.Repositories.Balances
{
    public class BalanceRepository : Repository<Balance, int>, IBalanceRepository
    {
        public BalanceRepository(IDBContext dbContext) : base(dbContext)
        {
        }
    }
}
