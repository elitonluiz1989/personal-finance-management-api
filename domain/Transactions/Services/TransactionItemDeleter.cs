using PersonalFinanceManagement.Domain.Balances.Contracts.Installments;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Balances.Enums;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Services;
using PersonalFinanceManagement.Domain.Transactions.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Domain.Transactions.Services
{
    public sealed class TransactionItemDeleter : BaseDeleter<TransactionItem, int, ITransactionItemRepository>, ITransactionItemDeleter
    {
        private readonly ITransactionItemStore _transactionItemStore;
        private readonly IInstallmentStore _installmentStore;

        public TransactionItemDeleter(
            INotificationService notificationService,
            ITransactionItemStore transactionItemStore,
            IInstallmentStore installmentStore,
            ITransactionItemRepository repository
        )
            : base(notificationService, repository)
        {
            _transactionItemStore = transactionItemStore;
            _installmentStore = installmentStore;
        }

        public override void Delete(TransactionItem? transactionItem)
        {
            if (transactionItem is null)
                return;

            HandleTransactionWithoutResidue(transactionItem);
            HandleTransactionWithResidue(transactionItem);
        }

        protected override async Task<TransactionItem?> Find(int id)
        {
            return await _repository.GetWithTransactionResidue(id);
        }

        private void HandleTransactionWithoutResidue(TransactionItem transactionItem)
        {
            if (transactionItem.IsResidue || transactionItem.Installment is null)
                return;

            UpdateInstallmentStatus(transactionItem.Installment);
        }

        private void HandleTransactionWithResidue(TransactionItem transactionItem)
        {
            if (!transactionItem.IsResidue || !transactionItem.TransactionResidues.Any())
                return;

            foreach (var transactionResidue in transactionItem.TransactionResidues)
            {
                if (transactionResidue.Installment is null)
                    continue;

                UpdateInstallmentStatus(transactionResidue.Installment);
            }
        }

        private void UpdateInstallmentStatus(Installment installment)
        {
            var status = HandleInstallmentStatus(installment);

            if (status is InstallmentStatusEnum.PartiallyPaid)
                UpdatePartiallyPaidOfLastTransactionItem(installment);

            _installmentStore.UpdateStatus(installment, status);
        }

        private static InstallmentStatusEnum HandleInstallmentStatus(Installment installment)
        {
            return installment.TransactionItems?.Count > 1
                ? InstallmentStatusEnum.PartiallyPaid
                : InstallmentStatusEnum.Created;
        }

        private void UpdatePartiallyPaidOfLastTransactionItem(Installment installment)
        {
            var transactionItem = installment.TransactionItems.LastOrDefault();

            if (transactionItem is null || transactionItem.PartiallyPaid is true)
                return;

            transactionItem.PartiallyPaid = true;

            _transactionItemStore.Store(transactionItem);
        }
    }
}
