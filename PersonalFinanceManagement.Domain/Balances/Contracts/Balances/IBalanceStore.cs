using PersonalFinanceManagement.Domain.Balances.Dtos;

namespace PersonalFinanceManagement.Domain.Balances.Contracts.Balances
{
    public interface IBalanceStore
    {
        Task Store(BalanceStoreDto dto, bool fromRefinance = false);
    }
}
