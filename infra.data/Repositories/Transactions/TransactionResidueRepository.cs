using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Infra.Data.Repositories.Transactions
{
    public class TransactionResidueRepository : Repository<TransactionResidue, int>, ITransactionResidueRepository
    {
        public TransactionResidueRepository(IDBContext dbContext) : base(dbContext) {}
    }
}
