using PersonalFinanceManagement.Domain.Balances.Contracts.Installments;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Services;
using PersonalFinanceManagement.Domain.Transactions.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Dtos;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Domain.Transactions.Services
{
    public class TransactionItemManager : NotifiableService, ITransactionItemManager
    {
        private readonly ITransactionItemStorageHandler _transactionItemStorageHandler;
        private readonly IBalancesAsTransactionValueHandler _balancesAsTransactionValueHandler;
        private readonly IInstallmentRepository _installmentRepository;
        private readonly ITransactionItemRepository _transactionItemRepository;

        public TransactionItemManager(
            INotificationService notificationService,
            ITransactionItemStorageHandler trasanctionItemStorageHandler,
            IBalancesAsTransactionValueHandler balancesAsTransactionValueHandler,
            IInstallmentRepository installmentRepository,
            ITransactionItemRepository transactionItemRepository
        )
            : base(notificationService)
        {
            _transactionItemStorageHandler = trasanctionItemStorageHandler;
            _balancesAsTransactionValueHandler = balancesAsTransactionValueHandler;
            _installmentRepository = installmentRepository;
            _transactionItemRepository = transactionItemRepository;
        }

        public async Task Manage(TransactionStoreDto dto, Transaction transaction)
        {
            if (Validate(dto, transaction) is false)
                return;

            DeleteExistingItems(transaction);

            var installments = await GetInstallments(dto.InstallmentsIds, dto.UserId);

            if (HasNotifications)
                return;

            await HandleWhenToUseBalancesAsValue(dto, transaction, installments);

            if (HasNotifications)
                return;

            var transactionItemStorageDto = new TransactionItemStorageDto()
            {
                UserId = dto.UserId,
                Date = dto.Date,
                Amount = transaction.Amount,
                CanGenerateCredit = !dto.UseBalancesAsValue
            };

            await _transactionItemStorageHandler.Handle(transactionItemStorageDto, transaction, installments);
        }

        private bool Validate(TransactionStoreDto dto, Transaction transaction)
        {
            if (ValidateNullableObject(dto) is false || ValidateNullableObject(transaction) is false)
                return false;

            return true;
        }

        private void DeleteExistingItems(Transaction transacation)
        {
            if (transacation.Items.Any() is false)
                return;

            _transactionItemRepository.Delete(transacation.Items);
        }

        private async Task<List<Installment>> GetInstallments(int[] installmentsIds, int userId)
        {
            var installments = await _installmentRepository.ListWithTransactionItems(installmentsIds, userId);

            if (installments is null || installments.Any() is false)
            {
                AddNotification("None of the reported installment weren't found. Maybe they don't belong to the user or aren't active.");

                return new();
            }

            var retrievedInstallmentsIds = installments.Select(i => i.Id).ToArray();
            var unretrievedInstallmentsIds = installmentsIds.Where(i => retrievedInstallmentsIds.Contains(i) is false).ToArray();

            if (unretrievedInstallmentsIds?.Any() is true)
            {
                var unretrievedIds = string.Join(", ", unretrievedInstallmentsIds);

                AddNotification(
                    $"The following list of reported installments IDs was not found. Maybe they don't belong to the user or aren't active: {unretrievedIds}"
                );

                return new();
            }

            return installments;
        }

        private async Task HandleWhenToUseBalancesAsValue(TransactionStoreDto dto, Transaction transaction, List<Installment> installments)
        {
            if (dto.UseBalancesAsValue is false)
                return;

            if (dto.BalancesIds is null || dto.BalancesIds.Any() is false)
            {
                AddNotification("Balances are required when marked to use them as transaction value.");

                return;
            }

            var balancesAsTransactionValueDto = new BalancesAsTransactionValueDto()
            {
                UserId = dto.UserId,
                Date = dto.Date,
                Amount = installments.Sum(i => i.Amount),
                BalancesIds = dto.BalancesIds
            };

            transaction.Amount = await _balancesAsTransactionValueHandler.Handle(balancesAsTransactionValueDto, transaction);
        }
    }
}
