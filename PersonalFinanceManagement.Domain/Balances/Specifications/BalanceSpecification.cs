using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Balances.Contracts;
using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Filters;

namespace PersonalFinanceManagement.Domain.Balances.Specifications
{
    public class BalanceSpecification : IBalanceSpecification
    {
        private readonly IBalanceRepository _repository;

        public BalanceSpecification(IBalanceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BalanceDto>> Get(BalanceFilter filter)
        {
            var query = _repository.Query();

            if (filter.Id > 0)
                query = query.Where(p => p.Id == filter.Id);

            if (filter.UserId > 0)
                query = query.Where(p => p.UserId == filter.UserId);

            if (filter.Types.Any())
                query = query.Where(p => filter.Types.Contains(p.Type));

            if (filter.Status.Any())
                query = query.Where(p => filter.Status.Contains(p.Status));

            if (filter.Financed.HasValue)
                query = query.Where(p =>  p.Financed == filter.Financed);

            if (filter.NumberOfInstallments.HasValue)
                query = query.Where(p => p.NumberOfInstallments == p.NumberOfInstallments);

            return await query.Select(s => new BalanceDto()
            {
                Id = s.Id,
                UserId = s.UserId,
                Type = s.Type,
                Status = s.Status,
                Value = s.Value,
                Financed = s.Financed,
                NumberOfInstallments = s.NumberOfInstallments
            })
            .ToListAsync();
        }
    }
}
