using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinanceManagement.Domain.Balances.Entities;

namespace PersonalFinanceManagement.Infra.Data.Mappings
{
    internal class TransactionItemMapping : IEntityTypeConfiguration<TransactionItem>
    {
        public void Configure(EntityTypeBuilder<TransactionItem> builder)
        {
            builder.ToTable("TransactionItems");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnOrder(1);

            builder.Property(p => p.TransactionId)
                .HasColumnOrder(2);

            builder.Property(p => p.InstallmentId)
                .HasColumnOrder(3);

            builder.Property(p => p.PartiallyPaid)
                .HasColumnOrder(4)
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasOne(p => p.Transaction)
                .WithMany(o => o.Items)
                .HasForeignKey(p => p.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Installment)
                .WithMany(o => o.Items)
                .HasForeignKey(p => p.InstallmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(p => p.Errors);
        }
    }
}
