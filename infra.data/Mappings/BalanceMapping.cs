using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinanceManagement.Domain.Balances.Entities;

namespace PersonalFinanceManagement.Infra.Data.Mappings
{
    internal class BalanceMapping : BaseMapping<Balance, int>, IEntityTypeConfiguration<Balance>
    {
        public void Configure(EntityTypeBuilder<Balance> builder)
        {
            builder.ToTable("Balances");

            BaseConfigure(builder);

            builder.Property(p => p.UserId)
                .HasColumnOrder(1);

            builder.Property(p => p.Name)
                .HasColumnOrder(2)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Type)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(p => p.Date)
                .HasColumnOrder(4)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(p => p.Amount)
                .HasColumnOrder(5)
                .IsRequired();

            builder.Property(p => p.Financed)
                .HasColumnOrder(6)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(p => p.InstallmentsNumber)
                .HasColumnOrder(7);

            builder.Property(p => p.CreatedAt)
                .HasColumnOrder(8)
                .IsRequired();

            builder.Property(p => p.UpadtedAt)
                .HasColumnOrder(9)
                .IsRequired(false);

            builder.Property(p => p.DeletedAt)
                .HasColumnOrder(10)
                .IsRequired(false);

            builder.HasOne(p => p.User)
                .WithMany(o => o.Balances)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(p => p.Closed);
        }
    }
}
