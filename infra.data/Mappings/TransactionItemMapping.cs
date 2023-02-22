using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinanceManagement.Domain.Transactions.Entities;
using PersonalFinanceManagement.Domain.Transactions.Enums;

namespace PersonalFinanceManagement.Infra.Data.Mappings
{
    internal class TransactionItemMapping : BaseMapping<TransactionItem, int>, IEntityTypeConfiguration<TransactionItem>
    {
        public void Configure(EntityTypeBuilder<TransactionItem> builder)
        {
            builder.ToTable("TransactionItems");

            BaseConfigure(builder);

            builder.Property(p => p.TransactionId)
                .HasColumnOrder(1);

            builder.Property(p => p.InstallmentId)
                .HasColumnOrder(2);

            builder.Property(p => p.Type)
                .HasColumnOrder(3)
                .HasDefaultValue(TransactionItemTypeEnum.Standard)
                .IsRequired();

            builder.Property(p => p.PartiallyPaid)
                .HasColumnOrder(4)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(p => p.AmountPaid)
                .HasColumnOrder(5)
                .IsRequired();

            builder.HasOne(p => p.Transaction)
                .WithMany(o => o.Items)
                .HasForeignKey(p => p.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Installment)
                .WithMany(o => o.TransactionItems)
                .HasForeignKey(p => p.InstallmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
