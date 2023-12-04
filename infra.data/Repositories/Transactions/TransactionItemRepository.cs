using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Infra.Data.Repositories.Transactions
{
    public class TransactionItemRepository : Repository<TransactionItem, int>, ITransactionItemRepository
    {
        public TransactionItemRepository(IDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<TransactionItem?> GetWithTransactionResidue(int id)
        {
            return await _context.Set<TransactionItem>()
                .Include(p => p.Installment)
                .Include(p => p.TransactionResidues)
                    .ThenInclude(p => p!.Installment)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
