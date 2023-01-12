using PersonalFinanceManagement.Domain.Balances.Dtos;

namespace PersonalFinanceManagement.Domain.Balances.Contracts.RefinanceBalances
{
    public interface IRefinancedBalanceStore
    {
        Task Store(RefinancedBalanceStoreDto dto, int userId);
    }
}
