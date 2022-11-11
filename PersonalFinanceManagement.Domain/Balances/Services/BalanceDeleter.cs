using PersonalFinanceManagement.Domain.Balances.Contracts;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Services;

namespace PersonalFinanceManagement.Domain.Balances.Services
{
    public class BalanceDeleter : BaseDeleter<Balance, int>, IBalanceDeleter
    {
        public BalanceDeleter(
            INotificationService notificationService,
            IBalanceRepository repository
        )
            :base(notificationService, repository)
        {
        }

        protected override void Validate(IEntity? entity)
        {
            if (entity is not Balance balance)
            {
                _notificationService.AddNotification($"{nameof(entity)} is null");

                return;
            }

            if (balance.Installments.Any())
            {
                _notificationService.AddNotification("Balance has installments and can't to deleted.");

                return;
            }

            if (balance.Transactions.Any())
            {
                _notificationService.AddNotification("Balance has installments and can't to deleted.");
            }
        }
    }
}
