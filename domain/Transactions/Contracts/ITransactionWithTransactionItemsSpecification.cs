using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Dtos;
using PersonalFinanceManagement.Domain.Transactions.Entities;
using PersonalFinanceManagement.Domain.Transactions.Filters;

namespace PersonalFinanceManagement.Domain.Transactions.Contracts
{
    public interface ITransactionWithTransactionItemsSpecification : ISpecification<Transaction, int, TransactionFilter, TransactionForListingDto>
    {
    }
}
