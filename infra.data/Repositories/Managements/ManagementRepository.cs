using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Managements.Contracts;
using PersonalFinanceManagement.Domain.Managements.Entities;

namespace PersonalFinanceManagement.Infra.Data.Repositories.Managements
{
    public class ManagementRepository : Repository<Management, int>, IManagementRepository
    {
        public ManagementRepository(IDBContext dbContext) : base(dbContext) { }
    }
}
