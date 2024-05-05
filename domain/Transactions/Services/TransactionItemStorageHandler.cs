using PersonalFinanceManagement.Domain.Balances.Contracts.Balances;
using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Balances.Enums;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Enums;
using PersonalFinanceManagement.Domain.Base.Services;
using PersonalFinanceManagement.Domain.Transactions.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Dtos;
using PersonalFinanceManagement.Domain.Transactions.Entities;
using PersonalFinanceManagement.Domain.Transactions.Enums;

namespace PersonalFinanceManagement.Domain.Transactions.Services
{
    public class TransactionItemStorageHandler : NotifiableService, ITransactionItemStorageHandler
    {
        private readonly ITransactionItemStore _transactionItemStore;
        private readonly IBalanceStore _balanceStore;
        private readonly ITransactionResidueStore _transactionResidueStore;

        public TransactionItemStorageHandler(
            INotificationService notificationService,
            ITransactionItemStore transactionItemStore,
            IBalanceStore balanceStore,
            ITransactionResidueStore transactionResidueStore
        )
            : base(notificationService)
        {
            _transactionItemStore = transactionItemStore;
            _balanceStore = balanceStore;
            _transactionResidueStore = transactionResidueStore;
        }

        public async Task Handle(TransactionItemStorageDto dto, Transaction transaction, List<Installment> installments)
        {
            var amount = dto.Amount;

            foreach (var installment in installments)
            {
                var transactionItemDto = CreateTransactionItemStoreDto(installment, dto.Type, ref amount);
                var transactionItem = await _transactionItemStore.Store(transactionItemDto, transaction, installment);

                if (HasNotifications)
                    break;

                HandleTransactionResidueItems(transactionItem!, installment.TransactionItems);

                if (HasNotifications || transactionItem!.PartiallyPaid)
                    break;
            }

            if (HasNotifications)
                return;

            if (amount > 0 && dto.CanGenerateCredit)
                await CreateCreditBalance(dto, transaction, amount);
        }

        private void HandleTransactionResidueItems(TransactionItem currentTransactionItem, List<TransactionItem> transactionItems)
        {
            if (transactionItems is null || !transactionItems.Any())
                return;

            var transactionItemsWithResidue = transactionItems
                .Where(ti =>
                    ti.Type == TransactionItemTypeEnum.Residue &&
                    ti.IsRecorded
                )
                .ToList();

            if (!transactionItemsWithResidue.Any())
                return;

            foreach (var transactionItem in transactionItemsWithResidue)
            {
                _transactionResidueStore.Store(transactionItem, currentTransactionItem);

                if (HasNotifications)
                    break;
            }
        }

        private static TransactionItemStoreDto CreateTransactionItemStoreDto(
            Installment installment,
            TransactionItemTypeEnum type,
            ref decimal amount
        )
        {
            var installmentAmount = installment.GetAmountToTransactions();
            var transactionItemDto = new TransactionItemStoreDto
            {
                PartiallyPaid = amount < installmentAmount,
                Type = type
            };

            if (transactionItemDto.PartiallyPaid)
            {
                installment.Status = InstallmentStatusEnum.PartiallyPaid;
                transactionItemDto.AmountPaid = amount;
                amount = 0;
            }
            else
            {
                if (type != TransactionItemTypeEnum.Residue) {
                    installment.Status = InstallmentStatusEnum.Paid;
                }

                transactionItemDto.AmountPaid = installmentAmount;
                amount -= installmentAmount;
            }

            return transactionItemDto;
        }

        private async Task CreateCreditBalance(TransactionItemStorageDto dto, Transaction transaction, decimal remainingValue)
        {
            var balanceStoreDto = new BalanceStoreDto()
            {
                UserId = dto.UserId,
                Name = $"Trasanction residue of {dto.Date:dd/MM/yyyy}",
                Type = CommonTypeEnum.Credit,
                Date = dto.Date,
                Amount = remainingValue,
                Financed = false,
                Residue = true
            };

            var balance = await _balanceStore.StoreFromTransaction(balanceStoreDto);

            if (balance is null)
            {
                AddNotification("Error on creating credit balance");

                return;
            }


            dto.CanGenerateCredit = false;
            dto.Type = TransactionItemTypeEnum.Residue;
            dto.Amount = remainingValue;

            await Handle(dto, transaction, balance.Installments);
        }
    }
}
