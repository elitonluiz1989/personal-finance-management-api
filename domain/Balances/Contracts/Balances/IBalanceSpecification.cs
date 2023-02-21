using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Filters;

namespace PersonalFinanceManagement.Domain.Balances.Contracts.Balances
{
    public interface IBalanceSpecification
    {
        Task<List<BalanceDto>> Get(BalanceFilter filter, int userId);
        Task<BalanceDto?> Find(int id, int userId);
    }
}
