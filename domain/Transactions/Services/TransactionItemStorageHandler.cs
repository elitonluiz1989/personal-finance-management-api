using PersonalFinanceManagement.Domain.Balances.Contracts.Balances;
using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Balances.Enums;
using PersonalFinanceManagement.Domain.Base.Contracts;
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

        public TransactionItemStorageHandler(
            INotificationService notificationService,
            ITransactionItemStore transactionItemStore,
            IBalanceStore balanceStore
        )
            : base(notificationService)
        {
            _transactionItemStore = transactionItemStore;
            _balanceStore = balanceStore;
        }

        public async Task Handle(TransactionItemStorageDto dto, Transaction transaction, List<Installment> installments)
        {
            var amount = dto.Amount;

            foreach (var installment in installments)
            {
                var transactionItemDto = CreateTransactionItemStoreDto(installment, dto.InstallmentAsTransactionAmount, ref amount);

                await _transactionItemStore.Store(transactionItemDto, transaction, installment);

                if (transactionItemDto.PartiallyPaid || HasNotifications)
                    break;
            }

            if (HasNotifications)
                return;

            if (amount > 0 && dto.CanGenerateCredit)
                await CreateCreditBalance(dto, transaction, amount);
        }

        private static TransactionItemStoreDto CreateTransactionItemStoreDto(Installment installment, bool installmentAsTransactionAmount, ref decimal amount)
        {
            var installmentAmount = installment.GetAmountToTransactions();
            var transactionItemDto = new TransactionItemStoreDto()
            {
                PartiallyPaid = amount < installmentAmount,
                Type = installmentAsTransactionAmount ?
                    TransactionItemTypeEnum.UsedAsTransactionAmount :
                    TransactionItemTypeEnum.Standard
            };

            if (transactionItemDto.PartiallyPaid)
            {
                installment.Status = InstallmentStatusEnum.PartiallyPaid;
                transactionItemDto.AmountPaid = amount;
                amount = 0;
            }
            else
            {
                installment.Status = InstallmentStatusEnum.Paid;
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
                Name = $"Remaining payment balance of {dto.Date:dd/MM/yyyy}",
                Type = BalanceTypeEnum.Credit,
                Date = dto.Date,
                Amount = remainingValue,
                Financed = false
            };

            var balance = await _balanceStore.StoreFromTransaction(balanceStoreDto);

            if (balance is null)
            {
                AddNotification("Error on creating credit balance");

                return;
            }


            dto.CanGenerateCredit = false;
            dto.InstallmentAsTransactionAmount = false;
            dto.Amount = remainingValue;

            await Handle(dto, transaction, balance.Installments);
        }
    }
}
