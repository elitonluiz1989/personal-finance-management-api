using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Specifications;
using PersonalFinanceManagement.Domain.Transactions.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Dtos;
using PersonalFinanceManagement.Domain.Transactions.Entities;
using PersonalFinanceManagement.Domain.Transactions.Extensions;
using PersonalFinanceManagement.Domain.Transactions.Filters;

namespace PersonalFinanceManagement.Domain.Transactions.Specifications
{
    public class TransactionWithTransactionItemsSpecification : Specification<Transaction, int, TransactionFilter, TransactionForListingDto>, ITransactionWithTransactionItemsSpecification
    {
        public TransactionWithTransactionItemsSpecification(ITransactionRepository repository)
            : base(repository)
        {
        }

        public override ISpecification<Transaction, int, TransactionFilter, TransactionForListingDto> WithFilter(
            TransactionFilter filter,
            int authenticatedUserId,
            bool isAdmin
        )
        {
            return this;
        }

        protected override IQueryable<TransactionForListingDto> GetQuery()
        {
            return Query
                .Include(p => p.User)
                .Include(p => p.TransactionItems)
                    .ThenInclude(p => p.Installment)
                        .ThenInclude(p => p!.Balance)
                .Select(s => s.ToTransactionForListingDto());
        }
    }
}
