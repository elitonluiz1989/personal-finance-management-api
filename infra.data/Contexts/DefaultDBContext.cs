using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Infra.Data.Helpers;
using PersonalFinanceManagement.Infra.Data.Mappings;

namespace PersonalFinanceManagement.Infra.Data.Contexts
{
    public class DefaultDBContext : DbContext, IDBContext
    {
        public static string ConnectionString { get; set; } = string.Empty;

        public DefaultDBContext()
        {
        }

        public DefaultDBContext(DbContextOptions<DefaultDBContext> options): base(options)
        {
        }

        public static void Configure(IServiceCollection services, string connectionString)
        {
            ConnectionString = connectionString;

            services.AddDbContext<DefaultDBContext>();
            services.AddScoped(typeof(IDBContext), provider => provider.GetService<DefaultDBContext>()!);
        }

        public static void InitializeDatabase(IServiceProvider provider)
        {
            var context = provider.CreateScope().ServiceProvider.GetRequiredService<DefaultDBContext>();
            context.Database.Migrate();
        }

        public static async Task Seed(IServiceProvider provider, IConfiguration? configuration)
        {
            var scope = provider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DefaultDBContext>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            await SeederHelpers.Run(context, unitOfWork, configuration);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                ConnectionString,
                b => b.MigrationsAssembly(typeof(DefaultDBContext).Assembly.FullName)
            );

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntitiesMapping.Mapping(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
