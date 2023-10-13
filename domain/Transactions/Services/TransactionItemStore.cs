using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Services;
using PersonalFinanceManagement.Domain.Transactions.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Dtos;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Domain.Transactions.Services
{
    public class TransactionItemStore : Store<TransactionItem, int>, ITransactionItemStore
    {
        public TransactionItemStore(
            INotificationService notificationService,
            ITransactionItemRepository repository
        )
            : base(notificationService, repository)
        {
        }

        public async Task Store(TransactionItemStoreDto dto, Transaction transaction, Installment installment)
        {
            if (ValidateDto(dto) is false)
                return;

            var transactionItem = await SetTransactionItem(dto, transaction, installment);

            if (HasNotifications || transactionItem is null)
                return;

            if (ValidateEntity(transactionItem) is false)
                return;

            SaveEntity(transactionItem);
        }

        private async Task<TransactionItem?> SetTransactionItem(TransactionItemStoreDto dto, Transaction transaction, Installment installment)
        {
            var transactionItem = new TransactionItem();

            if (dto.IsRecorded)
            {
                transactionItem = await _repository.Find(dto.Id);

                if (ValidateNullableObject(transactionItem) is false || transactionItem is null)
                    return default;
            }

            transactionItem.Transaction = transaction;
            transactionItem.Installment = installment;
            transactionItem.Type = dto.Type;
            transactionItem.PartiallyPaid = dto.PartiallyPaid;
            transactionItem.AmountPaid = dto.AmountPaid;

            return transactionItem;
        }
    }
}
