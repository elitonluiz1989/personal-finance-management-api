using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Balances.Contracts.RefinanceBalances;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Infra.Data.Repositories.Balances
{
    public class RefinancedBalanceRepository : Repository<RefinancedBalance, int>, IRefinancedBalanceRepository
    {
        public RefinancedBalanceRepository(IDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<RefinancedBalance>> GetAllByBalanceId(int balanceId)
        {
            return await Query()
                .Where(rb => rb.BalanceId == balanceId)
                .ToListAsync();
        }
    }
}
