using PersonalFinanceManagement.Domain.Balances.Contracts.Installments;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Infra.Data.Repositories.Balances
{
    public class InstallmentRepository : Repository<Installment, int>, IInstallmentRepository
    {
        public InstallmentRepository(IDBContext dbContext) : base(dbContext)
        {
        }
    }
}
