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

            builder.Property(p => p.Type)
                .IsRequired();

            builder.Property(p => p.Reference)
                .IsRequired();

            builder.Property(p => p.Date)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(p => p.Value)
                .IsRequired();

            builder.Property(p => p.DeletedAt)
                .IsRequired(false);

            builder.HasOne(p => p.Balance)
                .WithMany(o => o.Transactions)
                .HasForeignKey(p => p.BalanceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(p => p.Errors);
        }
    }
}
