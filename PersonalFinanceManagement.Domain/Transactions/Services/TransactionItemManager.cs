using PersonalFinanceManagement.Domain.Balances.Contracts.Balances;
using PersonalFinanceManagement.Domain.Balances.Contracts.Installments;
using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Balances.Enums;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Services;
using PersonalFinanceManagement.Domain.Transactions.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Dtos;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Domain.Transactions.Services
{
    public class TransactionItemManager : NotifiableService, ITransactionItemManager
    {
        private readonly IBalanceStore _balanceStore;
        private readonly ITransactionItemStore _transactionItemStore;
        private readonly IInstallmentRepository _installmentRepository;
        private readonly ITransactionItemRepository _transactionItemRepository;

        public TransactionItemManager(
            INotificationService notificationService,
            IBalanceStore balanceStore,
            ITransactionItemStore transactionItemStore,
            IInstallmentRepository installmentRepository,
            ITransactionItemRepository transactionItemRepository
        )
            : base(notificationService)
        {
            _balanceStore = balanceStore;
            _transactionItemStore = transactionItemStore;
            _installmentRepository = installmentRepository;
            _transactionItemRepository = transactionItemRepository;
        }

        public async Task Manage(TransactionStoreDto dto, Transaction transaction)
        {
            DeleteExistingItems(transaction);

            var remainingValue = transaction.Value;

            foreach (var installmentId in dto.InstallmentsIds)
            {
                var installment = await GetInstallment(installmentId, dto.UserId);

                if (installment is null)
                    break;

                var transactionItemDto = new TransactionItemStoreDto()
                {
                    InstallmentId = installment.Id,
                };
                var result = ManageItemContent(transactionItemDto, installment, remainingValue);

                remainingValue = result.remainingValue;

                await _transactionItemStore.Store(transactionItemDto, transaction);

                if (result.partiallyPaid || HasNotifications)
                    break;
            }

            if (remainingValue > 0)
                await CreateCreditBalance(dto, remainingValue);
        }

        private void DeleteExistingItems(Transaction transacation)
        {
            if (transacation.Items.Any() is false)
                return;

            _transactionItemRepository.Delete(transacation.Items);
        }

        private async Task<Installment?> GetInstallment(int installmentId, int userId)
        {
            var installment = await _installmentRepository.FindByUserIdWithTransactionItems(installmentId, userId);

            if (installment is null)
                AddNotification($"{nameof(installment)} is null");

            return installment;
        }

        private static (decimal remainingValue, bool partiallyPaid) ManageItemContent(
            TransactionItemStoreDto transactionItemDto,
            Installment installment,
            decimal remainingValue
        )
        {
            var value = installment.GetComputedValue();
            var partiallyPaid = remainingValue < value;

            transactionItemDto.PartiallyPaid = partiallyPaid;
            installment.Status = partiallyPaid ? InstallmentStatusEnum.PartiallyPaid : InstallmentStatusEnum.Paid;

            if (partiallyPaid)
            {
                transactionItemDto.AmountPaid = remainingValue;
                remainingValue = 0;
            }
            else
            {
                transactionItemDto.AmountPaid = value;
                remainingValue -= value;
            }

            return (remainingValue, partiallyPaid);
        }

        private async Task CreateCreditBalance(TransactionStoreDto dto, decimal remainingValue)
        {
            var balanceDto = new BalanceDto()
            {
                UserId = dto.UserId,
                Name = $"Remaining payment balance of {dto.Date:dd/MM/yyyy}",
                Type = BalanceTypeEnum.Credit,
                Status = BalanceStatusEnum.Open,
                Date = dto.Date,
                Value = remainingValue,
                Financed = false
            };

            await _balanceStore.Store(balanceDto);
        }
    }
}
