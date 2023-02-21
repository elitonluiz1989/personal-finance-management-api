using PersonalFinanceManagement.Domain.Balances.Contracts.Balances;
using PersonalFinanceManagement.Domain.Balances.Contracts.Installments;
using PersonalFinanceManagement.Domain.Balances.Contracts.RefinanceBalances;
using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Services;
using PersonalFinanceManagement.Domain.Users.Contracts;

namespace PersonalFinanceManagement.Domain.Balances.Services.RefinancedBalances
{
    public class RefinancedBalanceStore : NotifiableService, IRefinancedBalanceStore
    {
        private readonly IBalanceRepository _balanceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IInstallmentRepository _installmentRepository;
        private readonly IRefinancedBalanceRepository _repository;
        private readonly IBalanceStore _balanceStore;

        public RefinancedBalanceStore(
            INotificationService notificationService,
            IBalanceRepository balanceRepository,
            IUserRepository userRepository,
            IInstallmentRepository installmentRepository,
            IBalanceStore balanceStore,
            IRefinancedBalanceRepository repository
        )
            : base(notificationService)
        {
            _balanceRepository = balanceRepository;
            _userRepository = userRepository;
            _installmentRepository = installmentRepository;
            _balanceStore = balanceStore;
            _repository = repository;
        }

        public async Task Store(RefinancedBalanceStoreDto dto, int userId)
        {
            if (ValidateDto(dto) is false)
                return;

            var balance = await _balanceRepository.FindWithInstallments(dto.BalanceId);

            if (ValidateEntity(balance) is false || balance is null)
                return;

            var refinancedBalance = await SetRefinancedBalance(dto, balance, userId);

            if (HasNotifications || refinancedBalance is null)
                return;

            SaveRefinancing(refinancedBalance);

            if (HasNotifications)
                return;

            await SaveNewBalance(balance, refinancedBalance);

        }

        private bool ValidateDto(RefinancedBalanceStoreDto dto)
        {
            if (dto is not null)
                return true;

            AddNotification($"${nameof(RefinancedBalanceStoreDto)} is null");

            return false;
        }

        private bool ValidateEntity(Balance? balance)
        {
            if (balance is not null)
                return true;

            AddNotification($"{nameof(Balance)} is null");

            return false;
        }

        private async Task<RefinancedBalance?> SetRefinancedBalance(RefinancedBalanceStoreDto dto, Balance balance, int userId)
        {
            await InactivatePreviousRefinancing(balance.Id);

            return new RefinancedBalance
            {
                Balance = balance,
                OriginalDate = balance.Date,
                OriginalAmount = balance.Amount,
                OriginalFinanced = balance.Financed,
                OriginalInstallmentsNumber = balance.InstallmentsNumber,
                Date = SetBalanceDate(balance, dto),
                Amount = SetBalanceValue(balance, dto),
                Financed = SetBalanceFinanced(balance, dto),
                InstallmentsNumber = SetInstallmentsNumber(balance, dto),
                Active = true,
                CreatedAt = DateTime.Now,
                UserId = userId
            };
        }

        private async Task InactivatePreviousRefinancing(int balanaceId)
        {
            var refinancedBalances = await _repository.GetAllByBalanceId(balanaceId);

            if (refinancedBalances.Any() is false)
                return;

            foreach (var refinancedBalance in refinancedBalances)
            {
                refinancedBalance.Active = false;
            }
        }

        private static DateTime SetBalanceDate(Balance balance, RefinancedBalanceStoreDto dto)
        {
            if (dto.Date.Equals(default) is false && balance.Date != dto.Date)
                return dto.Date;

            return balance.Date;
        }

        private static decimal SetBalanceValue(Balance balance, RefinancedBalanceStoreDto dto)
        {
            if (dto.Amount.Equals(default) is false && balance.Amount != dto.Amount)
                return dto.Amount;

            return balance.Amount;
        }

        private static bool SetBalanceFinanced(Balance balance, RefinancedBalanceStoreDto dto)
        {
            return (balance.Financed != dto.Financed) ? dto.Financed : balance.Financed;
        }

        private static short SetInstallmentsNumber(Balance balance, RefinancedBalanceStoreDto dto)
        {
            if (dto.Financed is false)
                return 1;

            return (balance.InstallmentsNumber != dto.InstallmentsNumber) ? dto.InstallmentsNumber : balance.InstallmentsNumber;
        }

        private void SaveRefinancing(RefinancedBalance refinancedBalance)
        {
            if (refinancedBalance.Validate() is false)
            {
                NotificationService.AddNotification(refinancedBalance.Errors);

                return;
            }
            
            _repository.Save(refinancedBalance);
        }

        private async Task SaveNewBalance(Balance balance, RefinancedBalance refinancedBalance)
        {
            DeleteOldInstallments(balance);

            var balanceStoreDto = CreateBalanceToRefinance(balance, refinancedBalance);

            await _balanceStore.Store(balanceStoreDto, true);
        }

        private void DeleteOldInstallments(Balance balance)
        {
            if (balance.Installments.Any() is false)
                return;

            _installmentRepository.Delete(balance.Installments);
        }

        private static BalanceStoreDto CreateBalanceToRefinance(Balance balance, RefinancedBalance refinancedBalance)
        {
            return new BalanceStoreDto()
            {
                Id = balance.Id,
                Name = balance.Name,
                Type = balance.Type,
                Date = refinancedBalance.Date,
                Amount = refinancedBalance.Amount,
                Financed = refinancedBalance.Financed,
                InstallmentsNumber = refinancedBalance.InstallmentsNumber
            };
        }
    }
}
