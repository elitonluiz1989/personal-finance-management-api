using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Managements.Entities;

namespace PersonalFinanceManagement.Domain.Managements.Contracts
{
    public interface IManagementRepository : IRepository<Management, int>
    {
    }
}
