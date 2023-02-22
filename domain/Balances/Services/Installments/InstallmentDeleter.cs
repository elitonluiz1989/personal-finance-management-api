using PersonalFinanceManagement.Domain.Balances.Contracts.Installments;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Balances.Extensions;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Services;

namespace PersonalFinanceManagement.Domain.Balances.Services.Installments
{
    public class InstallmentDeleter : BaseDeleter<Installment, int, IInstallmentRepository>, IInstallmentDeleter
    {
        public InstallmentDeleter(
            INotificationService notificationService,
            IInstallmentRepository repository
        )
            : base(notificationService, repository)
        {
        }

        protected override async Task<Installment?> Find(int id)
        {
            return await _repository.FindWithTransactionItems(id);
        }

        protected override void Validate(IEntity? entity)
        {
            if (entity is not Installment installment)
            {
                _notificationService.AddNotification($"{nameof(Installment)} is null");

                return;
            }

            if (installment.TransactionItems?.Any() is true)
                _notificationService.AddNotification($"The installment of {installment.Reference.ToMonthYear()} has transactions and cannot to be deleted.");
        }
    }
}
