using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinanceManagement.Domain.Transactions.Entities;

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

            builder.Property(p => p.PartiallyPaid)
                .HasColumnOrder(3)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(p => p.AmountPaid)
                .HasColumnOrder(4)
                .IsRequired();

            builder.HasOne(p => p.Transaction)
                .WithMany(o => o.Items)
                .HasForeignKey(p => p.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Installment)
                .WithMany(o => o.Items)
                .HasForeignKey(p => p.InstallmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
