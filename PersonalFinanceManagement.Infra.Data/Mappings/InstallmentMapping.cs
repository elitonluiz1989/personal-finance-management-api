using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinanceManagement.Domain.Balances.Entities;

namespace PersonalFinanceManagement.Infra.Data.Mappings
{
    internal class InstallmentMapping : IEntityTypeConfiguration<Installment>
    {
        public void Configure(EntityTypeBuilder<Installment> builder)
        {
            builder.ToTable("Installments");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Reference)
                .IsRequired();

            builder.Property(p => p.Number)
                .IsRequired();

            builder.Property(p => p.Value)
                .IsRequired();

            builder.Property(p => p.DeletedAt)
                .IsRequired(false);

            builder.HasOne(p => p.Balance)
                .WithMany(o => o.Installments)
                .HasForeignKey(p => p.BalanceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(p => p.Errors);
        }
    }
}
