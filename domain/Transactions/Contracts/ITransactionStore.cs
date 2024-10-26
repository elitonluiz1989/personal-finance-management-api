using PersonalFinanceManagement.Domain.Transactions.Dtos;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Domain.Transactions.Contracts
{
    public interface ITransactionStore
    {
        Task Store(TransactionStoreDto dto);
    }
}
