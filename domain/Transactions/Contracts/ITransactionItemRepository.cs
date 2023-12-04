using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Domain.Transactions.Contracts
{
    public interface ITransactionItemRepository : IRepository<TransactionItem, int>
    {
        Task<TransactionItem?> GetWithTransactionResidue(int id);
    }
}
