using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinanceManagement.Domain.Balances.Entities;

namespace PersonalFinanceManagement.Infra.Data.Mappings
{
    internal class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnOrder(1);

            builder.Property(p => p.UserId)
                .HasColumnOrder(2);

            builder.Property(p => p.Type)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(p => p.Date)
                .HasColumnOrder(4)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(p => p.Value)
                .HasColumnOrder(5)
                .IsRequired();

            builder.Property(p => p.DeletedAt)
                .HasColumnOrder(6)
                .IsRequired(false);

            builder.Ignore(p => p.Errors);
        }
    }
}
