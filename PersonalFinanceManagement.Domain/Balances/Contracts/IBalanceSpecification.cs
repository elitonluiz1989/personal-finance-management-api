using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Filters;

namespace PersonalFinanceManagement.Domain.Balances.Contracts
{
    public interface IBalanceSpecification
    {
        Task<List<BalanceDto>> Get(BalanceFilter filter);
    }
}
