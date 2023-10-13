using PersonalFinanceManagement.Domain.Balances.Contracts.Installments;
using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Domain.Balances.Services.Installments
{
    public class BalanceInstallmentStore : IBalanceInstallmentStore
    {
        private readonly INotificationService _notificationService;
        private readonly IInstallmentStore _installmentStore;

        public BalanceInstallmentStore(
            INotificationService notificationService,
            IInstallmentStore installmentStore
        )
        {
            _notificationService = notificationService;
            _installmentStore = installmentStore;
        }

        public async Task Store(Balance balance, bool financed, short? installmentsNumber)
        {
            if (financed is false || installmentsNumber.HasValue is false)
            {
                await CreateSingleInstallment(balance);

                return;
            }

            await FinanceBalance(balance, installmentsNumber.Value);
        }

        private async Task CreateSingleInstallment(Balance balance)
        {
            var installment = CreateInstallmentDto(balance.Date, balance.Amount, number: 1, singleInstallment: true);

            await _installmentStore.Store(installment, balance);
        }

        private async Task FinanceBalance(Balance balance, short installmentsNumber)
        {
            short number = 1;
            var (amount, remainingAmount) = GetFinancedValue(balance.Amount, installmentsNumber);

            while (number <= installmentsNumber)
            {
                var installment = CreateInstallmentDto(balance.Date, amount, number);

                if (number == installmentsNumber && remainingAmount > 0)
                    installment.Amount += remainingAmount;

                await _installmentStore.Store(installment, balance);

                if (_notificationService.HasNotifications())
                    break;

                number++;
            }
        }

        private static InstallmentStoreDto CreateInstallmentDto(DateTime balanceDate, decimal value, short number, bool singleInstallment = false)
        {
            return new InstallmentStoreDto()
            {
                Reference = GetReference(balanceDate, number, singleInstallment),
                Number = number,
                Amount = value
            };
        }

        private static int GetReference(DateTime balanceDate, short number, bool singleInstallment)
        {
            var referenceDate = singleInstallment ? balanceDate : balanceDate.AddMonths(number);
            var referenceString = referenceDate.ToString("yyyyMM");

            return Convert.ToInt32(referenceString);
        }

        private static (decimal amount, decimal remainingAmount) GetFinancedValue(decimal balanceAmount, short installmentsNumber)
        {
            var modulus = balanceAmount % installmentsNumber;
            var amount = balanceAmount / installmentsNumber;
            var valueWithTwoDecimalPlaces = Math.Truncate(amount * 100) / 100;
            decimal remainingAmount = 0;

            if (modulus > 0)
            {
                var totalFinancedValue = valueWithTwoDecimalPlaces * installmentsNumber;

                remainingAmount = balanceAmount - totalFinancedValue;
            }

            return (valueWithTwoDecimalPlaces, remainingAmount);
        }
    }
}
