using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Balances.Contracts.Installments;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Infra.Data.Repositories.Balances
{
    public class InstallmentRepository : Repository<Installment, int>, IInstallmentRepository
    {
        public InstallmentRepository(IDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<Installment?> FindByUserIdWithTransactionItems(int balanceId, int userId)
        {
            return await Query()
                .Include(i => i.Balance)
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => 
                    i.Id == balanceId &&
                    i.Balance != null &&
                    i.Balance.UserId == userId
                );
        }
    }
}
