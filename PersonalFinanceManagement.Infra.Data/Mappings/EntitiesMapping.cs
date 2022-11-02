using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Infra.Data.Mappings
{
    internal static class EntitiesMapping
    {
        internal static void Mapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(new UserMapping().Configure);
        }
    }
}
