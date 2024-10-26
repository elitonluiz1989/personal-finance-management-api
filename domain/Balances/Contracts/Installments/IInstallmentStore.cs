﻿using PersonalFinanceManagement.Domain.Balances.Contracts.Balances;
using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Balances.Enums;

namespace PersonalFinanceManagement.Domain.Balances.Contracts.Installments
{
    public interface IInstallmentStore
    {
        void Store(Installment? installment);
        Task Store(InstallmentStoreDto dto, Balance balance);
        Task Store(InstallmentStoreDto dto);
        void UpdateStatus(Installment installment, InstallmentStatusEnum status);
        void UpdateStatus(IEnumerable<Installment> installments, InstallmentStatusEnum status);
    }
}
