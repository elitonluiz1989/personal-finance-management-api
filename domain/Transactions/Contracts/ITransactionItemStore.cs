using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Transactions.Dtos;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Domain.Transactions.Contracts
{
    public interface ITransactionItemStore
    {
        TransactionItem? Store(TransactionItem transactionItem);
        Task<TransactionItem?> Store(TransactionItemStoreDto dto, Transaction transaction, Installment installment);
    }
}
