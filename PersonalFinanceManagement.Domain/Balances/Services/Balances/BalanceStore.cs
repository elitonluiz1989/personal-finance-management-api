using PersonalFinanceManagement.Domain.Balances.Contracts.Balances;
using PersonalFinanceManagement.Domain.Balances.Contracts.Installments;
using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Services;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Domain.Balances.Services.Balances
{
    public class BalanceStore : Store<Balance, int>, IBalanceStore
    {
        private readonly IUserRepository _userRepository;
        private readonly IBalanceInstallmentStore _balanceInstallmentStoreService;

        public BalanceStore(
            INotificationService notificationService,
            IBalanceRepository repository,
            IUserRepository userRepository,
            IBalanceInstallmentStore balanceInstallmentStoreService
        )
            : base(notificationService, repository)
        {
            _userRepository = userRepository;
            _balanceInstallmentStoreService = balanceInstallmentStoreService;
        }

        public async Task Store(BalanceDto dto, int userId, bool fromRefinance = false)
        {
            if (ValidateDto(dto) is false)
                return;

            dto.UserId = userId;

            var balance = await SetBalance(dto, fromRefinance);

            if (ValidateEntity(balance) is false || balance is null)
                return;

            await FinanceBalance(balance, dto, fromRefinance);

            if (HasNotifications)
                return;

            SaveEntity(balance);
        }


        protected async Task<Balance?> SetBalance(BalanceDto dto, bool fromRefinance = false)
        {
            if (HasNotifications)
                return null;

            if (dto.IsRecorded)
                return await UpdateBalance(dto, fromRefinance);

            return await CreateBalance(dto);
        }

        private async Task<Balance?> CreateBalance(BalanceDto balanceDto)
        {
            var user = await GetUser(balanceDto.UserId);

            if (HasNotifications)
                return null;

            return new Balance()
            {
                User = user,
                Name = balanceDto.Name,
                Type = balanceDto.Type,
                Status = balanceDto.Status,
                Date = balanceDto.Date,
                Value = balanceDto.Value,
                Financed = balanceDto.Financed,
                InstallmentsNumber = balanceDto.InstallmentsNumberValidate
            };
        }

        private async Task<Balance?> UpdateBalance(BalanceDto balanceDto, bool fromRefinance = false)
        {
            var balance = await _repository.Find(balanceDto.Id);

            if (balance is null)
            {
                AddNotification($"{nameof(balance)} is null");

                return null;
            }

            if (balance.Name != balanceDto.Name)
                balance.Name = balanceDto.Name;

            if (balanceDto.Type != default && balance.Type != balanceDto.Type)
                balance.Type = balanceDto.Type;

            if (balanceDto.Status != default && balance.Status != balanceDto.Status)
                balance.Status = balanceDto.Status;

            if (fromRefinance)
            {
                if (balance.Date != balanceDto.Date)
                    balance.Date = balanceDto.Date;

                if (balance.Value != balanceDto.Value)
                    balance.Value = balanceDto.Value;

                if (balance.Financed != balanceDto.Financed)
                    balance.Financed = balanceDto.Financed;

                if (balance.InstallmentsNumber != balanceDto.InstallmentsNumber)
                    balance.InstallmentsNumber = balanceDto.InstallmentsNumber;
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

        private async Task FinanceBalance(Balance balance, BalanceDto dto, bool fromRefinance = false)
        {
            if (balance.IsRecorded && fromRefinance is false)
                return;

            await _balanceInstallmentStoreService.Store(balance, dto.Financed, dto.InstallmentsNumber);
        }
    }
}
