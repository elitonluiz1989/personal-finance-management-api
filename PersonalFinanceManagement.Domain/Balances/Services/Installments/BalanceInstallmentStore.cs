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
            var installment = CreateInstallmentDto(balance.Date, balance.Value);

            await _installmentStore.Store(installment, balance);
        }

        private async Task FinanceBalance(Balance balance, short installmentsNumber)
        {
            short number = 1;
            var financedValue = GetFinancedValue(balance.Value, installmentsNumber);

            while (number <= installmentsNumber)
            {
                var installment = CreateInstallmentDto(balance.Date, financedValue.Value, number);

                if (number == installmentsNumber && financedValue.Remainder > 0)
                    installment.Value += financedValue.Remainder;

                await _installmentStore.Store(installment, balance);

                if (_notificationService.HasNotifications())
                    break;

                number++;
            }
        }

        private static InstallmentStoreDto CreateInstallmentDto(DateTime balanceDate, decimal value, short number = 1)
        {
            return new InstallmentStoreDto()
            {
                Reference = GetReference(balanceDate, number),
                Number = number,
                Value = value
            };
        }

        private static int GetReference(DateTime balanceDate, short number)
        {
            var referenceDate = balanceDate.AddMonths(number);
            var referenceString = referenceDate.ToString("yyyyMM");

            return Convert.ToInt32(referenceString);
        }

        private static (decimal Value, decimal Remainder) GetFinancedValue(decimal balanceValue, short installmentsNumber)
        {
            var modulus = balanceValue % installmentsNumber;
            var value = balanceValue / installmentsNumber;
            var valueWithTwoDecimalPlaces = Math.Truncate(value * 100) / 100;
            decimal remainder = 0;

            if (modulus > 0)
            {
                var totalFinancedValue = valueWithTwoDecimalPlaces * installmentsNumber;

                remainder = balanceValue - totalFinancedValue;
            }

            return (valueWithTwoDecimalPlaces, remainder);
        }
    }
}
