﻿using PersonalFinanceManagement.Domain.Balances.Entities;

namespace PersonalFinanceManagement.Domain.Balances.Contracts.Installments
{
    public interface IBalanceInstallmentStore
    {
        Task Store(Balance balance, bool Financed, short? installmentsNumber);
    }
}
