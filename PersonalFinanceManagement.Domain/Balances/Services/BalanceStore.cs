using PersonalFinanceManagement.Domain.Balances.Contracts;
using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Dtos;
using PersonalFinanceManagement.Domain.Base.Services;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Domain.Balances.Services
{
    public class BalanceStore : BaseStore<Balance, int>, IBalanceStore
    {
        private readonly IUserRepository _userRepository;

        public BalanceStore(
            INotificationService notificationService,
            IBalanceRepository repository,
            IUserRepository userRepository
        )
            : base(notificationService, repository)
        {
            _userRepository = userRepository;
        }

        protected override async Task<Balance?> SetEntity<TDto>(TDto dto)
        {
            var balanceDto = ConvertDto(dto);

            if (balanceDto is null || _notificationService.HasNotifications())
                return null;

            if (balanceDto.IsRecorded)
            {
                return await UpdateBalance(balanceDto);
            }

            return await CreateBalance(balanceDto);
        }

        private async Task<Balance?> CreateBalance(BalanceDto balanceDto)
        {
            var user = await GetUser(balanceDto.UserId);

            if (_notificationService.HasNotifications())
                return null;

            return new Balance()
            {
                User = user,
                Type = balanceDto.Type,
                Status = balanceDto.Status,
                Value = balanceDto.Value,
                Financed = balanceDto.Financed,
                NumberOfInstallments = balanceDto.NumberOfInstallments,
            };
        }

        private async Task<Balance?> UpdateBalance(BalanceDto balanceDto)
        {
            var balance = await _repository.Find(balanceDto.Id);

            if (balance is null)
            {
                _notificationService.AddNotification($"{nameof(balance)} is null");
            }

            if (_notificationService.HasNotifications())
                return null;

            if (balance.Type != balanceDto.Type)
                balance.Type = balanceDto.Type;

            if (balance.Status != balanceDto.Status)
                balance.Status = balanceDto.Status;

            if (balance.Value != balanceDto.Value)
                balance.Value = balanceDto.Value;

            if (balance.Financed != balanceDto.Financed)
                balance.Financed = balanceDto.Financed;

            if (balance.NumberOfInstallments != balanceDto.NumberOfInstallments)
                balance.NumberOfInstallments = balanceDto.NumberOfInstallments;

            return balance;
        }

        private BalanceDto? ConvertDto<TDto>(TDto dto) where TDto : RecordedDto<int>
        {
            var balanceDto = dto as BalanceDto;

            if (balanceDto is null)
            {
                _notificationService.AddNotification($"{nameof(balanceDto)} is null");
            }

            return balanceDto;
        }

        private async Task<User?> GetUser(int userId)
        {
            var user = await _userRepository.Find(userId);

            if (user is null)
                _notificationService.AddNotification($"{nameof(user)} is null");

            return user;
        }
    }
}
