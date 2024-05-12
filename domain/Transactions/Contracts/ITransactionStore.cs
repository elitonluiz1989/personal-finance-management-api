using PersonalFinanceManagement.Domain.Managements.Entities;
using PersonalFinanceManagement.Domain.Transactions.Dtos;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Domain.Transactions.Contracts
{
    public interface ITransactionStore
    {

        void Store(Transaction transaction);
        Task Store(TransactionStoreDto dto);
        void Store(Transaction transaction, Management management);
        Task Store(int[] transactionsIds, Management management);
    }
}
