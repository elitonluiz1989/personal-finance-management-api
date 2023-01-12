using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinanceManagement.Domain.Base.Entites;

namespace PersonalFinanceManagement.Infra.Data.Mappings
{
    internal class BaseMapping<IEntity, TId>
            where IEntity : Entity<TId>
            where TId : struct
    {
        internal static void BaseConfigure(EntityTypeBuilder<IEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnOrder(0);

            builder.Ignore(p => p.Errors);
            builder.Ignore(p => p.IsRecorded);
            builder.Ignore(p => p.WithRegistrationDates);
            builder.Ignore(p => p.WithSoftDelete);
        }
    }
}
