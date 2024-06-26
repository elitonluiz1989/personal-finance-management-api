﻿using PersonalFinanceManagement.Domain.Managements.Dtos;
using PersonalFinanceManagement.Domain.Managements.Filters;

namespace PersonalFinanceManagement.Domain.Managements.Contracts
{
    public interface IManagementStore
    {
        Task Store(ManagementStoreDto dto);
        Task Store(ManagementStoreFilter filter);
    }
}
