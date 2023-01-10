using PersonalFinanceManagement.Domain.Transactions.Dtos;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Domain.Transactions.Contracts
{
    public interface IBalancesAsTransactionValueHandler
    {
        Task<decimal> Handle(BalancesAsTransactionValueDto dto, Transaction transaction);
    }
}
