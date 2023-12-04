using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Infra.Data.Repositories.Transactions
{
    public class TransactionRepository : Repository<Transaction, int>, ITransactionRepository
    {
        public TransactionRepository(IDBContext dbContext) : base(dbContext) { }

        public async Task<Transaction?> GetTransactionWithTransactionItem(int id)
        {
            var transaction = await _context.Set<Transaction>()
                .Include(p => p.TransactionItems)
                    .ThenInclude(p => p.Installment)
                .Include(p => p.TransactionItems)
                    .ThenInclude(p => p.TransactionResidues)
                        .ThenInclude(p => p.TransactionItem)
                            .ThenInclude(p => p!.Transaction)
                                .ThenInclude(p => p!.TransactionItems)
                                    .ThenInclude(p => p.Installment)
                                        .ThenInclude(p => p!.Balance)
                .FirstOrDefaultAsync(p => p.Id == id);
            
            return transaction;
        }
    }
}
