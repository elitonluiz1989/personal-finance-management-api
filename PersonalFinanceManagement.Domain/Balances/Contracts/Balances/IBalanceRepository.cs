using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Domain.Balances.Contracts.Balances
{
    public interface IBalanceRepository : IRepository<Balance, int>
    {
        Task<Balance?> FindWithInstallments(int balanceId);
        Task<List<Balance>> ListWithInstallmentsByIds(int[] ids, int userId);
    }
}
