using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Balances.Contracts.Balances;
using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Balances.Extensions;
using PersonalFinanceManagement.Domain.Balances.Filters;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Specifications;

namespace PersonalFinanceManagement.Domain.Balances.Specifications
{
    public class BalanceSpecification : Specification<Balance, int, BalanceFilter, BalanceDto>, IBalanceSpecification
    {
        public BalanceSpecification(IBalanceRepository repository)
        : base(repository)
        {
        }

        public override ISpecification<Balance, int, BalanceFilter, BalanceDto> WithFilter(BalanceFilter filter, int authenticatedUserId, bool isAdmin)
        {
            if (filter.Id > 0)
                Query.Where(p => p.Id == filter.Id);

            if (filter.UserId > 0)
                Query.Where(p => p.UserId == filter.UserId);

            if (filter.Types.Any())
                Query.Where(p => filter.Types.Contains(p.Type));

            if (filter.Financed.HasValue)
                Query.Where(p => p.Financed == filter.Financed);

            if (filter.InstallmentsNumber.HasValue)
                Query.Where(p => p.InstallmentsNumber == p.InstallmentsNumber);

            if (filter.Closed.HasValue)
                Query.Where(p => p.Closed == filter.Closed);

            if (filter.UserId > 0)
            {
                Query.Where(p => p.UserId == filter.UserId);
            }
            else
            {                
                if (isAdmin is false)
                   Query.Where(p => p.UserId == authenticatedUserId);
            }

            return this;
        }

        protected override IQueryable<BalanceDto> GetQuery()
        {
            return Query
                .Include(p => p.Installments)
                .Select(s => BalanceMappingsExtension.ToBalanceDto(s));
        }
    }
}
