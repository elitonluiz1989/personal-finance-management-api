using PersonalFinanceManagement.Domain.Balances.Contracts.Balances;
using PersonalFinanceManagement.Domain.Balances.Contracts.Installments;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Services;

namespace PersonalFinanceManagement.Domain.Balances.Services.Balances
{
    public class BalanceDeleter : BaseDeleter<Balance, int, IBalanceRepository>, IBalanceDeleter
    {
        private readonly IInstallmentDeleter _installmentDeleter;
        private bool _allowDeleteResidue = false;

        public BalanceDeleter(
            INotificationService notificationService,
            IInstallmentDeleter installmentDeleter,
            IBalanceRepository repository
        )
            : base(notificationService, repository)
        {
            _installmentDeleter = installmentDeleter;
        }

        public override void Delete(Balance? balance)
        {
            Validate(balance);

            if (balance is null || _notificationService.HasNotifications())
                return;

            if (balance.Installments?.Any() is true)
                _installmentDeleter.Delete(balance.Installments, _allowDeleteResidue);


            if (_notificationService.HasNotifications())
                return;

            _repository.Delete(balance);
        }

        public void Delete(Balance? balance, bool allowDeleteResidue)
        {
            _allowDeleteResidue = allowDeleteResidue;

            Delete(balance);
        }

        protected override async Task<Balance?> Find(int id)
        {
            return await _repository.FindWithTransactions(id);
        }

        protected override void Validate(Balance? entity)
        {
            if (entity is not Balance balance)
            {
                _notificationService.AddNotification($"{nameof(Balance)} is null");

                return;
            }

            if (balance.HasTransactions && _allowDeleteResidue is false)
            {
                _notificationService.AddNotification(
                    "The balance has transactions and cannot to be deleted."
                );
            }
        }
    }
}
