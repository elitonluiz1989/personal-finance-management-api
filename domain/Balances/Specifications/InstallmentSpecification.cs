using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Balances.Contracts.Installments;
using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Balances.Extensions;
using PersonalFinanceManagement.Domain.Balances.Filters;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Specifications;

namespace PersonalFinanceManagement.Domain.Balances.Specifications
{
    public class InstallmentSpecification : Specification<Installment, int, InstallmentFilter, InstallmentDto>, IInstallmentSpecification
    {
        private InstallmentFilter _filter = new();
        public InstallmentSpecification(IInstallmentRepository repository)
            : base(repository)
        {
        }

        public override ISpecification<Installment, int, InstallmentFilter, InstallmentDto> WithFilter(InstallmentFilter filter, int authenticatedUserId, bool isAdmin)
        {
            _filter = filter;

            if (_filter.Id > 0)
                _query = _query.Where(p => p.Id == filter.Id);

            if (_filter.BalanceId > 0)
                _query = _query.Where(p => p.BalanceId == filter.BalanceId);

            if (_filter.UserId > 0)
            {
                _query = _query.Where(p =>
                    p.Balance != null &&
                    p.Balance.UserId == filter.UserId
                );
            }
            else
            {
                if (isAdmin is false)
                    _query = _query.Where(p =>
                        p.Balance != null &&
                        p.Balance.UserId == authenticatedUserId
                    );
            }

            if (_filter.TransactionId > 0)
                _query = _query.Where(p =>
                    p.TransactionItems.Any(ti => 
                        ti.Transaction != null &&
                        ti.Transaction.Id == filter.TransactionId
                    )
                );

            if (_filter.Reference > 0)
                _query = _query.Where(p => p.Reference == filter.Reference);

            if (_filter.Number > 0)
                _query = _query.Where(p => p.Number == filter.Number);

            if (_filter.Status > 0)
                _query = _query.Where(p => p.Status == p.Status);

            if (_filter.Amount > 0)
                _query = _query.Where(p => p.Amount == filter.Amount);

            return this;
        }

        public override async Task<IEnumerable<InstallmentDto>> List()
        {
            return await GetQuery().ToListAsync();
        }

        public override async Task<InstallmentDto?> First()
        {
            return await GetQuery().FirstOrDefaultAsync();
        }

        private IQueryable<InstallmentDto> GetQuery()
        {
            _query = _query
                .Include(p => p.Balance)
                .Include(p => p.TransactionItems)
                    .ThenInclude(p => p.Transaction);

            if (_filter.WithoutPagination is false)
            {
                _query = _query
                    .Skip(_filter.Page)
                    .Take(_filter.PageSize);
            }

            return _query
                .Select(s => InstalmentMappingsExtension.ToInstallmentDto(s));
        }
    }
}
