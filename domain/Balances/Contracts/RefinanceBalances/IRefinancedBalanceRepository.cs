using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Domain.Balances.Contracts.RefinanceBalances
{
    public interface IRefinancedBalanceRepository : IRepository<RefinancedBalance, int>
    {
        Task<List<RefinancedBalance>> GetAllByBalanceId(int balanceId);
    }
}
