using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Domain.Transactions.Contracts
{
    public interface ITransactionDeleter : IBaseDeleter<Transaction, int>
    {
    }
}
