using PersonalFinanceManagement.Domain.Balances.Contracts.Balances;
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
    public class BalancesAsTransactionValueHandler : NotifiableService, IBalancesAsTransactionValueHandler
    {
        private readonly ITransactionItemStorageHandler _transactiontemStorageHandler;
        private readonly IBalanceRepository _balanceRepository;

        public BalancesAsTransactionValueHandler(
            INotificationService notificationService,
            ITransactionItemStorageHandler transactiontemStorageHandler,
            IBalanceRepository balanceRepository
        )
            : base(notificationService)
        {
            _transactiontemStorageHandler = transactiontemStorageHandler;
            _balanceRepository = balanceRepository;
        }

        public async Task<decimal> Handle(BalancesAsTransactionValueDto dto, Transaction transaction)
        {
            if (ValidateNullableObject(dto) is false || ValidateNullableObject(transaction) is false)
                return default;

            var balances = await GetBalances(dto.BalancesIds, dto.UserId, transaction.Type);

            if (HasNotifications)
                return default;

            var balancesAmount = balances.Sum(b => b.GetAmountToTransaction());

            await HandleValuesAndStatus(dto, transaction, balances);

            return balancesAmount;
        }

        private async Task<List<Balance>> GetBalances(int[] balancesIds, int userId, TransactionTypeEnum transactionType)
        {
            var results = await _balanceRepository.ListWithInstallmentsByIds(balancesIds, userId);
            var balances = results.Where(b => b.Closed is false).ToList();

            Validate(balances, balancesIds, transactionType);

            return balances;
        }

        private bool Validate(List<Balance> balances, int[] balancesIds, TransactionTypeEnum transactionType)
        {
            if (balances is null || balances.Any() is false)
            {
                AddNotification("None of the reported balances weren't found. Maybe it doesn't belong to the user or is closed.");

                return false;
            }

            var transactionTypeToBalanceType = transactionType == TransactionTypeEnum.Credit ? BalanceTypeEnum.Credit : BalanceTypeEnum.Debt;

            if (balances.Any(b => b.Type == transactionTypeToBalanceType))
            {
                var typeDescription = transactionTypeToBalanceType == BalanceTypeEnum.Credit ? "Credit" : "Debt";

                AddNotification($"Some types of reported balances are not compatible with {typeDescription} transactions.");

                return false;
            }

            var retrievedBalancesIds = balances.Select(b => b.Id).ToArray();
            var unretrievedBalancesIds = balancesIds.Where(b => retrievedBalancesIds.Contains(b) is false).ToArray();

            if (unretrievedBalancesIds?.Any() is true)
            {
                var unretrievedIds = string.Join(", ", unretrievedBalancesIds);

                AddNotification(
                    $"The following list of reported balances IDs was not found. Maybe they don't belong to the user or aren't active: {unretrievedIds}"
                );

                return false;
            }

            return true;
        }

        private async Task HandleValuesAndStatus(BalancesAsTransactionValueDto dto, Transaction transaction, List<Balance> balances)
        {
            var transactionItemStoradeDto = new TransactionItemStorageDto()
            {
                UserId = dto.UserId,
                Date = dto.Date,
                Amount = dto.Amount,
                CanGenerateCredit = false,
                InstallmentAsTransactionAmount = true
            };
            var installments = balances
                .SelectMany(b => b.Installments)
                .ToList();

            await _transactiontemStorageHandler.Handle(transactionItemStoradeDto, transaction, installments);
        }
    }
}
