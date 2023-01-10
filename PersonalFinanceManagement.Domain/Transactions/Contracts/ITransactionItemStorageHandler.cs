using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Transactions.Dtos;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Domain.Transactions.Contracts
{
    public interface ITransactionItemStorageHandler
    {
        Task Handle(TransactionItemStorageDto dto, Transaction transaction, List<Installment> installments);
    }
}
