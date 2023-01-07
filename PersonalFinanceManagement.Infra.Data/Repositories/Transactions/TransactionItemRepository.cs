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
    }
}
