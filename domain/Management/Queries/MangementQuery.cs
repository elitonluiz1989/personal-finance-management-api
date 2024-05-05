using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Balances.Enums;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Management.Dtos;
using PersonalFinanceManagement.Domain.Management.Enums;
using PersonalFinanceManagement.Domain.Transactions.Entities;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Domain.Management.Queries
{
    public static class MangementQuery
    {
        public static IQueryable<ManagementResult> GetRemainingInstallmentQuery(int reference, IDBContext _context)
        {
            var remainingInstallmentStatus = new InstallmentStatusEnum[]
            {
                InstallmentStatusEnum.Created,
                InstallmentStatusEnum.PartiallyPaid
            };
            var query =
                from i in GetInstallmentBaseQuery(_context)
                join til in _context.Set<TransactionItem>()
                    on i.Id equals til.InstallmentId into tit
                from ti in tit.DefaultIfEmpty()
                where
                    i.Reference < reference &&
                    (
                        i.Status == InstallmentStatusEnum.Created ||
                        ti.PartiallyPaid
                    )
                orderby
                    i.UserId,
                    i.Id
                select new ManagementResult
                {
                    Id = i.Id,
                    Reference = i.Reference,
                    Type = i.Type,
                    Status = i.Status,
                    Description = i.Description,
                    Amount = ti.PartiallyPaid
                        ? i.Amount - ti.AmountPaid
                        : i.Amount,
                    UserId = i.UserId,
                    UserName = i.UserName,
                    ManagementType = ManagementItemTypeEnum.RemainingValue
                };

            return query;
        }

        public static IQueryable<ManagementResult> GetInstallmentQuery(int reference, IDBContext context)
        {
            var query =
                from i in GetInstallmentBaseQuery(context)
                where
                    i.Reference == reference
                select i;
            var transctionQuery =
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
                    Description = $"Trancation of {t.Date:dd/MM/yyyy}",
                    Amount = t.Amount,
                    UserId = u.Id,
                    UserName = u.Name,
                    ManagementType = ManagementItemTypeEnum.Transaction
                };

            return query;
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
                    Description = $"Trancation of {t.Date:dd/MM/yyyy}",
                    Amount = t.Amount,
                    UserId = u.Id,
                    UserName = u.Name,
                    ManagementType = ManagementItemTypeEnum.Transaction
                };

            return query;
        }

        private static IQueryable<ManagementResult> GetInstallmentBaseQuery(IDBContext context)
        {
            return from u in context.Set<User>()
                   join b in context.Set<Balance>()
                       on u.Id equals b.UserId
                   join i in context.Set<Installment>()
                       on b.Id equals i.BalanceId
                   select new ManagementResult
                   {
                       Id = i.Id,
                       Reference = i.Reference,
                       Type = b.Type,
                       Status = i.Status,
                       Description = $"{b.Name} {i.Number}/{b.InstallmentsNumber}",
                       Amount = i.Amount,
                       ManagementType = ManagementItemTypeEnum.Installment,
                       UserId = u.Id,
                       UserName = u.Name
                   };
        }
    }
}
