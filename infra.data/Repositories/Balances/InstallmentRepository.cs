﻿using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Balances.Contracts.Installments;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Balances.Enums;
using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Infra.Data.Repositories.Balances
{
    public class InstallmentRepository : Repository<Installment, int>, IInstallmentRepository
    {
        public InstallmentRepository(IDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Installment>> ListWithTransactionItems(int[] ids, int userId)
        {
            return await Query()
                .Include(i => i.Balance)
                .Include(i => i.Items)
                .Where(i => 
                    ids.Contains(i.Id) &&
                    i.Status != InstallmentStatusEnum.Paid &&
                    i.Balance != null &&
                    i.Balance.UserId == userId
                )
                .ToListAsync();
        }
    }
}
