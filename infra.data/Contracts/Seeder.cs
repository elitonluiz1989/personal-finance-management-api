using Microsoft.Extensions.Configuration;
using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Infra.Data.Contracts
{
    internal abstract class Seeder
    {
        public IConfiguration? Configuration { get; set; }
        public bool RunsOnMigration { get; protected set; } = false;
        protected IDBContext _dbContext;
        protected IUnitOfWork UnitOfWork;

        public Seeder(
            IDBContext dbContext,
            IUnitOfWork unitOfWork
        )
        {
            _dbContext = dbContext;
            UnitOfWork = unitOfWork;
        }

        public abstract Task Run();
    }
}
