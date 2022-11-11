using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Domain.Balances.Contracts
{
    public interface IBalanceRepository : IRepository<Balance, int>
    {
    }
}
