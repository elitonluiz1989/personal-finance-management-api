using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;

namespace PersonalFinanceManagement.Domain.Balances.Contracts.Installments
{
    public interface IInstallmentStore
    {
        Task Store(InstallmentStoreDto dto);
        Task Store(InstallmentStoreDto dto, Balance balance);
    }
}
