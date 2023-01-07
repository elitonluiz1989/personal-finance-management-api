using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Infra.Data.Repositories.Transactions
{
    public class TransactionRepository : Repository<Transaction, int>, ITransactionRepository
    {
        public TransactionRepository(IDBContext dbContext) : base(dbContext)
        {
        }
    }
}
