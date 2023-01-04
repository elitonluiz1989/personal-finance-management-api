using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinanceManagement.Domain.Balances.Entities;

namespace PersonalFinanceManagement.Infra.Data.Mappings
{
    internal class RefinancedBalanceMapping : BaseMapping<RefinancedBalance, int>, IEntityTypeConfiguration<RefinancedBalance>
    {
        public void Configure(EntityTypeBuilder<RefinancedBalance> builder)
        {
            builder.ToTable("RefinancedBalances");

            BaseConfigure(builder);

            builder.Property(p => p.UserId)
                .HasColumnOrder(1);

            builder.Property(p => p.BalanceId)
                .HasColumnOrder(2);

            builder.Property(p => p.OriginalDate)
                .HasColumnOrder(3)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(p => p.OriginalValue)
                .HasColumnOrder(4)
                .IsRequired();

            builder.Property(p => p.OriginalFinanced)
                .HasColumnOrder(5)
                .IsRequired();

            builder.Property(p => p.OriginalInstallmentsNumber)
                .HasColumnOrder(6);

            builder.Property(p => p.Date)
                .HasColumnOrder(7)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(p => p.Value)
                .HasColumnOrder(8)
                .IsRequired();

            builder.Property(p => p.Financed)
                .HasColumnOrder(9)
                .IsRequired();

            builder.Property(p => p.InstallmentsNumber)
                .HasColumnOrder(10);

            builder.Property(p => p.Active)
                .HasDefaultValue(true)
                .HasColumnOrder(11);

            builder.Property(p => p.CreatedAt)
                .HasColumnOrder(12)
                .IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(o => o.RefinancedBalances)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Balance)
                .WithMany(o => o.RefinancedBalances)
                .HasForeignKey(p => p.BalanceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
