using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Domain.Balances.Contracts.Balances
{
    public interface IBalanceDeleter : IBaseDeleter<Balance, int>
    {
        void Delete(Balance? balance, bool allowDeleteResidue);
    }
}
