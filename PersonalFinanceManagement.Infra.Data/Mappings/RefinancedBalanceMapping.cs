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

            builder.Property(p => p.BalanceId)
                .HasColumnOrder(1);

            builder.Property(p => p.OriginalDate)
                .HasColumnOrder(2)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(p => p.OriginalValue)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(p => p.OriginalFinanced)
                .HasColumnOrder(4)
                .IsRequired();

            builder.Property(p => p.OriginalInstallmentsNumber)
                .HasColumnOrder(5);

            builder.Property(p => p.Date)
                .HasColumnOrder(6)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(p => p.Value)
                .HasColumnOrder(7)
                .IsRequired();

            builder.Property(p => p.Financed)
                .HasColumnOrder(8)
                .IsRequired();

            builder.Property(p => p.InstallmentsNumber)
                .HasColumnOrder(9);

            builder.Property(p => p.Active)
                .HasDefaultValue(true)
                .HasColumnOrder(10);

            builder.Property(p => p.CreatedAt)
                .HasColumnOrder(11)
                .IsRequired();

            builder.HasOne(p => p.Balance)
                .WithMany(o => o.RefinancedBalances)
                .HasForeignKey(p => p.BalanceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
