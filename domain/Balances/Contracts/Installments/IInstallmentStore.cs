using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Balances.Enums;

namespace PersonalFinanceManagement.Domain.Balances.Contracts.Installments
{
    public interface IInstallmentStore
    {
        Task Store(InstallmentStoreDto dto);
        Task Store(InstallmentStoreDto dto, Balance balance);
        void UpdateStatus(Installment installment, InstallmentStatusEnum status);
        void UpdateStatus(IEnumerable<Installment> installments, InstallmentStatusEnum status);
    }
}
