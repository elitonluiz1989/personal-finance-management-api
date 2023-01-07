using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Infra.Data.Mappings
{
    internal class TransactionMapping : BaseMapping<Transaction, int>, IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            BaseConfigure(builder);

            builder.Property(p => p.UserId)
                .HasColumnOrder(1);

            builder.Property(p => p.Type)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(p => p.Date)
                .HasColumnOrder(3)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(p => p.Value)
                .HasColumnOrder(4)
                .IsRequired();

            builder.Property(p => p.DeletedAt)
                .HasColumnOrder(5)
                .IsRequired(false);
        }
    }
}
