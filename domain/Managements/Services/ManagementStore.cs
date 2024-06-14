using PersonalFinanceManagement.Domain.Balances.Contracts.Installments;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Services;
using PersonalFinanceManagement.Domain.Managements.Contracts;
using PersonalFinanceManagement.Domain.Managements.Dtos;
using PersonalFinanceManagement.Domain.Managements.Entities;
using PersonalFinanceManagement.Domain.Managements.Filters;
using PersonalFinanceManagement.Domain.Transactions.Contracts;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Domain.Managements.Services
{
    public class ManagementStore : Store<Management, int>, IManagementStore
    {
        private readonly IManagementStoreSpecification _specification;
        private readonly IInstallmentStore _installmentStore;
        private readonly ITransactionStore _transactionStore;
        private readonly IUserRepository _userRepository;

        public ManagementStore(
            INotificationService notificationService,
            IManagementStoreSpecification specification,
            IInstallmentStore installmentStore,
            ITransactionStore transactionStore,
            IManagementRepository repository,
            IUserRepository userRepository
        )
            : base(notificationService, repository)
        {
            _specification = specification;
            _installmentStore = installmentStore;
            _transactionStore = transactionStore;
            _userRepository = userRepository;
        }

        public async Task Store(ManagementStoreDto dto)
        {
            if (ValidateDto(dto) is false)
                return;

            var management = await SetFinancialManagement(dto);

            if (ValidateNullableObject(management) is false)
                return;

            if (ValidateEntity(management) is false)
                return;

            await _installmentStore.Store(dto.InstallmentsIds, management!);

            if (HasNotifications)
                return;

            await _transactionStore.Store(dto.TransactionsIds, management!);

            if (HasNotifications)
                return;

            _repository.Save(management!);
        }

        public async Task Store(ManagementStoreFilter filter)
        {
            List<ManagementStoreDto> results = await _specification.Get(filter);

            foreach (var dto in results)
            {
                await Store(dto);

                if (HasNotifications)
                    return;
            }
        }

        private async Task<Management?> SetFinancialManagement(ManagementStoreDto dto)
        {
            if (dto.IsRecorded)
                return await GetFinancialManagement(dto);

            return await CreateFinancialManagement(dto);
        }

        private async Task<Management?> GetFinancialManagement(ManagementStoreDto dto)
        {
            var management = await _repository.Find(dto.Id);

            if (management is null)
                return default;

            management.InitialAmount = dto.InitialAmount;
            management.Amount = dto.Amount;

            return management;
        }

        private async Task<Management?> CreateFinancialManagement(ManagementStoreDto dto)
        {
            var user = await GetUser(dto.UserId);

            if (HasNotifications)
                return default;

            return new Management()
            {
                User = user,
                Reference = dto.Reference,
                InitialAmount = dto.InitialAmount,
                Amount = dto.Amount
            };
        }

        private async Task<User?> GetUser(int userId)
        {
            var user = await _userRepository.Find(userId);

            if (ValidateNullableObject(user) is false)
                return default;

            return user;
        }
    }
}
