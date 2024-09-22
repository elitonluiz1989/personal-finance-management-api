using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Extensions;
using PersonalFinanceManagement.Domain.Managements.Dtos;
using PersonalFinanceManagement.Domain.Managements.Enums;
using PersonalFinanceManagement.Domain.Managements.Filters;
using PersonalFinanceManagement.Domain.Transactions.Entities;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Domain.Managements.Queries
{
    public static class MangementQuery
    {
        public static IQueryable<ManagementResult> GetInstallmentQuery(int reference, IDBContext context)
        {
            return
                from u in context.Set<User>()
                join b in context.Set<Balance>()
                    on u.Id equals b.UserId
                join i in context.Set<Installment>()
                    on b.Id equals i.BalanceId
                where
                    i.Reference == reference &&
                    !b.Residue
                select new ManagementResult
                {
                    Id = i.Id,
                    Reference = i.Reference,
                    Type = b.Type,
                    ManagementType = ManagementItemTypeEnum.Installment,
                    Status = i.Status,
                    Date = b.Date.ToShortDateString(),
                    Description = $"{b.Name} {i.Number}/{b.InstallmentsNumber}",
                    Amount = i.Amount,
                    UserId = u.Id,
                    UserName = u.Name,
                    CreatedAt = b.CreatedAt
                };
        }

        public static IQueryable<ManagementStoreResult> GetInstallmentToStoreQuery(
            ManagementStoreFilter filter,
            IDBContext context
        )
        {
            return
                from b in context.Set<Balance>()
                join i in context.Set<Installment>()
                    on b.Id equals i.BalanceId
                where
                    i.Reference == filter.Reference &&
                    (!filter.UserId.HasValue || b.UserId == filter.UserId)
                select new ManagementStoreResult
                {
                    Id = i.Id,
                    Type = b.Type,
                    ManagementType = ManagementItemTypeEnum.Installment,
                    Amount = i.Amount,
                    UserId = b.UserId
                };
        }

        public static IQueryable<ManagementResult> GetTransactionQuery(int reference, IDBContext context)
        {
            var query =
                from u in context.Set<User>()
                join t in context.Set<Transaction>()
                    on u.Id equals t.UserId
                where
                    t.Reference == reference
                select new ManagementResult
                {
                    Id = t.Id,
                    Reference = Convert.ToInt32(t.Date.ToString("yyyyMM")),
                    Type = t.Type,
                    ManagementType = ManagementItemTypeEnum.Transaction,
                    Date = t.Date.ToShortDateString(),
                    Description = t.Type.GetDescription(),
                    Amount = t.Amount,
                    UserId = u.Id,
                    UserName = u.Name,
                    CreatedAt = t.CreatedAt
                };

            return query;
        }

        public static IQueryable<ManagementStoreResult> GetTransactionToStoreQuery(
            ManagementStoreFilter filter,
            IDBContext context
        )
        {
            return
                from t in context.Set<Transaction>()
                where
                    t.Reference == filter.Reference &&
                    (!filter.UserId.HasValue || t.UserId == filter.UserId)
                select new ManagementStoreResult
                {
                    Id = t.Id,
                    Type = t.Type,
                    ManagementType = ManagementItemTypeEnum.Transaction,
                    Amount = t.Amount,
                    UserId = t.UserId
                };
        }
    }
}
