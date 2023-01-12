using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Infra.Data.Mappings
{
    internal class UserMapping : BaseMapping<User, int>, IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            BaseConfigure(builder);

            builder.Property(p => p.Name)
                .HasColumnOrder(1)
                .IsRequired()
                .HasColumnType("varchar(200)")
                .HasMaxLength(200);

            builder.Property(p => p.UserName)
                .HasColumnOrder(2)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(p => p.Email)
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(p => p.Password)
                .HasColumnOrder(4)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(p => p.Role)
                .HasColumnOrder(5)
                .IsRequired()
                .HasConversion<short>();

            builder.HasIndex(p => p.UserName)
                .IsUnique();

            builder.HasIndex(p => p.Email)
                .IsUnique();
        }
    }
}
