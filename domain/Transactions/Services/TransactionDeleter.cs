using PersonalFinanceManagement.Domain.Balances.Contracts.Balances;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Services;
using PersonalFinanceManagement.Domain.Transactions.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Domain.Transactions.Services
{
    public sealed class TransactionDeleter : BaseDeleter<Transaction, int, ITransactionRepository>, ITransactionDeleter
    {
        private readonly ITransactionItemDeleter _transactionItemDeleter;
        private readonly IBalanceDeleter _balanceDeleter;

        public TransactionDeleter(
            INotificationService notificationService,
            ITransactionItemDeleter transactionItemDeleter,
            IBalanceDeleter balanceDeleter,
            ITransactionRepository repository
        )
            : base(notificationService, repository)
        {
            _transactionItemDeleter = transactionItemDeleter;
            _balanceDeleter = balanceDeleter;
        }

        public override void Delete(Transaction? transaction)
        {
            Validate(transaction);

            if (_notificationService.HasNotifications())
                return;

            foreach (var transactionItem in transaction!.TransactionItems)
            {
                DeleteTransactionResidues(transactionItem.TransactionResidues);

                if (_notificationService.HasNotifications())
                    break;

                _transactionItemDeleter.Delete(transactionItem);
            }

            if (_notificationService.HasNotifications())
                return;

            _repository.Delete(transaction);
        }

        private void DeleteTransactionResidues(List<TransactionResidue> transactionResidues)
        {
            if (!transactionResidues.Any())
                return;

            foreach (var transactionResidue in transactionResidues)
            {
                HandleTransactionItemOfResidue(transactionResidue);

                if (_notificationService.HasNotifications())
                    break;

                HandleBalanceResidue(transactionResidue);
            }
        }

        protected override async Task<Transaction?> Find(int id)
        {
            return await _repository.GetTransactionWithTransactionItem(id);
        }

        private void HandleTransactionItemOfResidue(TransactionResidue transactionResidue)
        {
            if (transactionResidue.Transaction?.TransactionItems?.Count == 1)
            {
                Delete(transactionResidue.Transaction);

                return;
            }
            
            _transactionItemDeleter.Delete(transactionResidue.TransactionItem);
        }

        private void HandleBalanceResidue(TransactionResidue transactionResidue)
        {
            if (transactionResidue.Balance?.Residue is false)
                return;

            _balanceDeleter.Delete(transactionResidue.Balance, allowDeleteResidue: true);
        }
    }
}
