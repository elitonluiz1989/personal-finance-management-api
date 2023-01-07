using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Domain.Balances.Contracts.Installments
{
    public interface IInstallmentRepository : IRepository<Installment, int>
    {
        Task<Installment?> FindByUserIdWithTransactionItems(int balanceId, int userId);
    }
}
