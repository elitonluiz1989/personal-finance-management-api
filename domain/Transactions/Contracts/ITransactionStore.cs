using PersonalFinanceManagement.Domain.Transactions.Dtos;

namespace PersonalFinanceManagement.Domain.Transactions.Contracts
{
    public interface ITransactionStore
    {
        Task Store(TransactionStoreDto dto);
    }
}
