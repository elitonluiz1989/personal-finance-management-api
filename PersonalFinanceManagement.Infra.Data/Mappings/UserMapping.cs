using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Infra.Data.Mappings
{
    internal class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnOrder(1);

            builder.Property(p => p.Name)
                .HasColumnOrder(2)
                .IsRequired()
                .HasColumnType("varchar(200)")
                .HasMaxLength(200);

            builder.Property(p => p.UserName)
                .HasColumnOrder(3)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(p => p.Email)
                .HasColumnOrder(4)
                .IsRequired();

            builder.Property(p => p.Password)
                .HasColumnOrder(5)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(p => p.Role)
                .HasColumnOrder(6)
                .IsRequired()
                .HasConversion<short>();

            builder.HasIndex(p => p.UserName)
                .IsUnique();

            builder.HasIndex(p => p.Email)
                .IsUnique();

            builder.Ignore(p => p.Errors);
        }
    }
}
