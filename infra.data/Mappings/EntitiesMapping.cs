using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Transactions.Entities;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Infra.Data.Mappings
{
    internal static class EntitiesMapping
    {
        internal static void Mapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(new UserMapping().Configure);

            modelBuilder.Entity<Balance>(new BalanceMapping().Configure);
            modelBuilder.Entity<Installment>(new InstallmentMapping().Configure);
            modelBuilder.Entity<Transaction>(new TransactionMapping().Configure);
            modelBuilder.Entity<TransactionItem>(new TransactionItemMapping().Configure);
            modelBuilder.Entity<RefinancedBalance>(new RefinancedBalanceMapping().Configure);
        }
    }
}
