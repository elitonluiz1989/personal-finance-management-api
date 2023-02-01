using PersonalFinanceManagement.Domain.Balances.Contracts.Balances;
using PersonalFinanceManagement.Domain.Balances.Contracts.Installments;
using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Balances.Extensions;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Services;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Domain.Balances.Services.Balances
{
    public class BalanceStore : Store<Balance, int>, IBalanceStore
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IBalanceInstallmentStore _balanceInstallmentStoreService;

        public BalanceStore(
            IUnitOfWork unitOfWork,
            INotificationService notificationService,
            IBalanceRepository repository,
            IUserRepository userRepository,
            IBalanceInstallmentStore balanceInstallmentStoreService
        )
            : base(notificationService, repository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _balanceInstallmentStoreService = balanceInstallmentStoreService;
        }

        public async Task<BalanceDto?> Store(BalanceStoreDto dto, bool fromRefinance = false)
        {
            if (ValidateDto(dto) is false)
                return default;

            var balance = await SetBalance(dto, fromRefinance);

            if (ValidateEntity(balance) is false || balance is null)
                return default;

            await FinanceBalance(balance, dto, fromRefinance);

            if (HasNotifications)
                return default;

            SaveEntity(balance);

            if (HasNotifications)
                return default;

            _unitOfWork.Commit();

            return balance.ToBalanceDto();
        }

        protected async Task<Balance?> SetBalance(BalanceStoreDto dto, bool fromRefinance = false)
        {
            if (HasNotifications)
                return null;

            if (dto.IsRecorded)
                return await UpdateBalance(dto, fromRefinance);

            return await CreateBalance(dto);
        }

        private async Task<Balance?> CreateBalance(BalanceStoreDto dto)
        {
            var user = await GetUser(dto.UserId);

            if (HasNotifications)
                return null;

            return new Balance()
            {
                User = user,
                Name = dto.Name,
                Type = dto.Type,
                Date = dto.Date,
                Amount = dto.Amount,
                Financed = dto.Financed,
                InstallmentsNumber = dto.InstallmentsNumberValidate
            };
        }

        private async Task<Balance?> UpdateBalance(BalanceStoreDto dto, bool fromRefinance = false)
        {
            var balance = await _repository.Find(dto.Id);

            if (balance is null)
            {
                AddNotification($"{nameof(balance)} is null");

                return null;
            }

            if (balance.Name != dto.Name)
                balance.Name = dto.Name;

            if (dto.Type != default && balance.Type != dto.Type)
                balance.Type = dto.Type;

            if (fromRefinance)
            {
                if (balance.Date != dto.Date)
                    balance.Date = dto.Date;

                if (balance.Amount != dto.Amount)
                    balance.Amount = dto.Amount;

                if (balance.Financed != dto.Financed)
                    balance.Financed = dto.Financed;

                if (balance.InstallmentsNumber != dto.InstallmentsNumber)
                    balance.InstallmentsNumber = dto.InstallmentsNumber;
            }

            return balance;
        }

        private async Task<User?> GetUser(int userId)
        {
            var user = await _userRepository.Find(userId);

            if (user is null)
                AddNotification($"{nameof(user)} is null");

            return user;
        }

        private async Task FinanceBalance(Balance balance, BalanceStoreDto dto, bool fromRefinance = false)
        {
            if (balance.IsRecorded && fromRefinance is false)
                return;

            await _balanceInstallmentStoreService.Store(balance, dto.Financed, dto.InstallmentsNumber);
        }
    }
}
