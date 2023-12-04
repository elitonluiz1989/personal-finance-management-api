using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Domain.Balances.Contracts.Installments
{
    public interface IInstallmentDeleter : IBaseDeleter<Installment, int>
    {
        void Delete(Installment? installment, bool allowDeleteResisdue);
        void Delete(IEnumerable<Installment> installment, bool allowDeleteResisdue);
    }
}
