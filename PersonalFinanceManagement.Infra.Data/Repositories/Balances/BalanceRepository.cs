using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Balances.Contracts.Balances;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Infra.Data.Repositories.Balances
{
    public class BalanceRepository : Repository<Balance, int>, IBalanceRepository
    {
        public BalanceRepository(IDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<Balance?> FindWithInstallments(int balanceId)
        {
            return await Query()
                .Include(b => b.Installments)
                .FirstOrDefaultAsync(b => b.Id == balanceId);
        }
    }
}
