using PersonalFinanceManagement.Domain.Balances.Dtos;

namespace PersonalFinanceManagement.Domain.Balances.Contracts.Balances
{
    public interface IBalanceStore
    {
        Task<BalanceDto?> Store(BalanceStoreDto dto, bool fromRefinance = false);
    }
}
