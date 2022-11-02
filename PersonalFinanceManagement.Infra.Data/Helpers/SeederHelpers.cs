using Microsoft.Extensions.Configuration;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Infra.Data.Contracts;

namespace PersonalFinanceManagement.Infra.Data.Helpers
{
    internal static class SeederHelpers
    {
        internal static async Task Run(IDBContext dbContext, IUnitOfWork unitOfWork, IConfiguration? configuration)
        {
            if (configuration is null)
                return;

            if (configuration["RunSeeds"] is null ||
                Convert.ToBoolean(configuration["RunSeeds"]) is false)
                return;

            var types = GetSeeders();

            if (types.Any() is false)
                return;

            foreach (var type in types)
            {
                if (Activator.CreateInstance(type, dbContext, unitOfWork) is not Seeder seeder || seeder.RunsOnMigration)
                    continue;

                seeder.Configuration = configuration;

                await seeder.Run();
            }
        }

        private static string GetSeedersNamespace()
        {
            var nodes = new string[] {
                nameof(PersonalFinanceManagement),
                nameof(Infra),
                nameof(Data),
                nameof(Seeders)
            };

            return string.Join(".", nodes);
        }

        private static Type[] GetSeeders()
        {
            var seedersNamespace = GetSeedersNamespace();
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t =>
                    string.IsNullOrEmpty(t.Namespace) is false &&
                    t.Namespace.StartsWith(seedersNamespace) &&
                    t.IsClass &&
                    t.IsAssignableTo(typeof(Seeder)) &&
                    t.IsAbstract is false
                )
                .ToArray();
        }
    }
}
