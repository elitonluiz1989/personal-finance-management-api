using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Balances.Contracts.Installments;
using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Balances.Extensions;
using PersonalFinanceManagement.Domain.Balances.Filters;
using PersonalFinanceManagement.Domain.Base.Dtos;
using PersonalFinanceManagement.Domain.Base.Specifications;

namespace PersonalFinanceManagement.Domain.Balances.Specifications
{
    public class InstallmentSpecification : Specification<Installment, int, InstallmentFilter, InstallmentDto>, IInstallmentSpecification
    {
        public InstallmentSpecification(IInstallmentRepository repository)
            : base(repository)
        {
            Filter = new InstallmentFilter();
        }

        public override IInstallmentSpecification WithFilter(InstallmentFilter filter, int authenticatedUserId, bool isAdmin)
        {
            Filter = filter;

            if (Filter is null)
                return this;

            if (Filter.Id > 0)
                Query = Query.Where(p => p.Id == filter.Id);

            if (Filter.BalanceId > 0)
                Query = Query.Where(p => p.BalanceId == filter.BalanceId);

            if (Filter.UserId > 0)
            {
                Query = Query.Where(p =>
                    p.Balance != null &&
                    p.Balance.UserId == filter.UserId
                );
            }
            else
            {
                if (isAdmin is false)
                    Query = Query.Where(p =>
                        p.Balance != null &&
                        p.Balance.UserId == authenticatedUserId
                    );
            }

            if (Filter.TransactionId > 0)
                Query = Query.Where(p =>
                    p.TransactionItems.Any(ti => 
                        ti.Transaction != null &&
                        ti.Transaction.Id == filter.TransactionId
                    )
                );

            if (Filter.BalanceType is not null)
            {
                Query = Query.Where(p =>
                    p.Balance != null &&
                    (
                        (
                            filter.InstallmentToAddAtTransaction == false &&
                            p.Balance.Type == Filter.BalanceType.Value
                        ) ||
                        (
                            filter.InstallmentToAddAtTransaction &&
                            p.Balance.Type != Filter.BalanceType.Value
                        )
                    )
                );
            }

            if (Filter.Reference > 0)
                Query = Query.Where(p => p.Reference == filter.Reference);

            if (Filter.Number > 0)
                Query = Query.Where(p => p.Number == filter.Number);

            if (Filter.Status > 0)
                Query = Query.Where(p => p.Status == p.Status);

            if (Filter.Amount > 0)
                Query = Query.Where(p => p.Amount == filter.Amount);

            return this;
        }

        public override async Task<PagedResultsDto<InstallmentDto>> PagedList()
        {
            var query = GetQuery();

            return await GetPagedResults(query);
        }

        public override async Task<InstallmentDto?> First()
        {
            return await GetQuery().FirstOrDefaultAsync();
        }

        protected override IQueryable<InstallmentDto> GetQuery()
        {
            return Query
                .Include(p => p.Balance)
                .Select(s => InstalmentMappingsExtension.ToInstallmentDto(s));
        }
    }
}
