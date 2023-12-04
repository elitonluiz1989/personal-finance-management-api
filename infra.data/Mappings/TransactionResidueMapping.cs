using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Infra.Data.Mappings
{
    internal class TransactionResidueMapping : BaseMapping<TransactionResidue, int>, IEntityTypeConfiguration<TransactionResidue>
    {
        public void Configure(EntityTypeBuilder<TransactionResidue> builder)
        {
            builder.ToTable(nameof(TransactionResidue));

            BaseConfigure(builder);

            builder.Property(p => p.TransactionItemOriginId)
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(p => p.TransactionItemId)
                .HasColumnOrder(2)
                .IsRequired();

            builder.HasOne(p => p.TransactionItemOrigin)
                .WithMany(p => p.TransactionResidues)
                .HasForeignKey(p => p.TransactionItemOriginId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.TransactionItem)
                .WithOne()
                .HasForeignKey<TransactionResidue>(p => p.TransactionItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(p => p.Transaction);
            builder.Ignore(p => p.Installment);
            builder.Ignore(p => p.Balance);
        }
    }
}
