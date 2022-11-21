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
        private readonly IBalanceInstallmentStoreService _balanceInstallmentStoreService;

        public BalanceStore(
            INotificationService notificationService,
            IBalanceRepository repository,
            IUserRepository userRepository,
            IBalanceInstallmentStoreService balanceInstallmentStoreService
        )
            : base(notificationService, repository)
        {
            _userRepository = userRepository;
            _balanceInstallmentStoreService = balanceInstallmentStoreService;
        }

        public async Task Store(BalanceDto dto)
        {
            if (ValidateDto(dto) is false)
                return;

            var balance = await SetBalance(dto);

            if (balance is null || ValidateEntity(balance) is false)
                return;

            await FinanceBalance(balance, dto);

            if (_notificationService.HasNotifications())
                return;

            SaveEntity(balance);
        }


        protected async Task<Balance?> SetBalance(BalanceDto dto)
        {
            if (_notificationService.HasNotifications())
                return null;

            if (dto.IsRecorded)
                return await UpdateBalance(dto);
                
            return await CreateBalance(dto);
        }

        private async Task<Balance?> CreateBalance(BalanceDto balanceDto)
        {
            var user = await GetUser(balanceDto.UserId);

            if (_notificationService.HasNotifications())
                return null;

            return new Balance()
            {
                User = user,
                Name = balanceDto.Name,
                Type = balanceDto.Type,
                Status = balanceDto.Status,
                Date= balanceDto.Date,
                Value = balanceDto.Value,
                Financed = balanceDto.Financed,
                InstallmentsNumber = balanceDto.InstallmentsNumber,
            };
        }

        private async Task<Balance?> UpdateBalance(BalanceDto balanceDto)
        {
            var balance = await _repository.Find(balanceDto.Id);

            if (balance is null)
            {
                _notificationService.AddNotification($"{nameof(balance)} is null");

                return null;
            }

            if (balance.Name != balanceDto.Name)
                balance.Name = balanceDto.Name;

            if (balanceDto.Type != default && balance.Type != balanceDto.Type)
                balance.Type = balanceDto.Type;

            if (balanceDto.Status != default && balance.Status != balanceDto.Status)
                balance.Status = balanceDto.Status;

            return balance;
        }

        private async Task<User?> GetUser(int userId)
        {
            var user = await _userRepository.Find(userId);

            if (user is null)
                _notificationService.AddNotification($"{nameof(user)} is null");

            return user;
        }

        private async Task FinanceBalance(Balance balance, BalanceDto dto)
        {
            if (balance.IsRecorded)
                return;

            await _balanceInstallmentStoreService.Store(balance, dto.Financed, dto.InstallmentsNumber);
        }
    }
}
