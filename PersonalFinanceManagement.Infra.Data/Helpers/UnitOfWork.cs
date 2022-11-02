using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Infra.Data.Helpers
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDBContext _dbContext;

        public UnitOfWork(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
