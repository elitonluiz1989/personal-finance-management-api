using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Balances.Enums;

namespace PersonalFinanceManagement.Infra.Data.Mappings
{
    internal class InstallmentMapping : BaseMapping<Installment, int>, IEntityTypeConfiguration<Installment>
    {
        public void Configure(EntityTypeBuilder<Installment> builder)
        {
            builder.ToTable("Installments");

            BaseConfigure(builder);

            builder.Property(p => p.BalanceId)
                .HasColumnOrder(1);

            builder.Property(p => p.Reference)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(p => p.Number)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(p => p.Status)
                .HasColumnOrder(4)
                .HasDefaultValue(InstallmentStatusEnum.Created)
                .IsRequired();

            builder.Property(p => p.Amount)
                .HasColumnOrder(5)
                .IsRequired();

            builder.Property(p => p.DeletedAt)
                .HasColumnOrder(6)
                .IsRequired(false);

            builder.HasOne(p => p.Balance)
                .WithMany(o => o.Installments)
                .HasForeignKey(p => p.BalanceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(p => p.Active);
        }
    }
}
