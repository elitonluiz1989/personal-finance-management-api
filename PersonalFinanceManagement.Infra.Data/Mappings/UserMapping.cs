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

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Guid)
                .IsRequired()
                .HasConversion<Guid>();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("VARCHAR(200)")
                .HasMaxLength(200);

            builder.Property(p => p.UserName)
                .IsRequired()
                .HasColumnType("VARCHAR(50)")
                .HasMaxLength(50);

            builder.Property(p => p.Email)
                .IsRequired();

            builder.Property(p => p.Password)
                .IsRequired()
                .HasColumnType("VARCHAR(50)")
                .HasMaxLength(50);

            builder.Property(p => p.Role)
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
