using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;

namespace PersonalFinanceManagement.Domain.Balances.Contracts.Balances
{
    public interface IBalanceStore
    {
        Task<BalanceDto?> Store(BalanceStoreDto dto, bool fromRefinance = false);
        Task<Balance?> StoreFromTransaction(BalanceStoreDto dto);
    }
}
