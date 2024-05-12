using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinanceManagement.Domain.Managements.Entities;

namespace PersonalFinanceManagement.Infra.Data.Mappings
{
    internal class ManagementMapping : BaseMapping<Management, int>, IEntityTypeConfiguration<Management>
    {
        public void Configure(EntityTypeBuilder<Management> builder)
        {
            BaseConfigure(builder);

            builder.Property(p => p.UserId)
                .HasColumnOrder(1);

            builder.Property(p => p.Reference)
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(p => p.InitialAmount)
                .HasColumnOrder(3)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(p => p.Amount)
                .HasColumnOrder(4)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .HasColumnOrder(5)
                .IsRequired();

            builder.Property(p => p.UpdatedAt)
                .HasColumnOrder(6)
                .IsRequired(false);

            builder.Property(p => p.DeletedAt)
                .HasColumnOrder(8)
                .IsRequired(false);
        }
    }
}
