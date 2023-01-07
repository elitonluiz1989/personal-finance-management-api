using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Services;
using PersonalFinanceManagement.Domain.Transactions.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Dtos;
using PersonalFinanceManagement.Domain.Transactions.Entities;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Domain.Transactions.Services
{
    public class TransactionStore : Store<Transaction, int>, ITransactionStore
    {
        private readonly ITransactionItemManager _transactionItemManager;
        private readonly IUserRepository _userRepository;

        public TransactionStore(
            INotificationService notificationService,
            ITransactionItemManager transactionItemManager,
            ITransactionRepository repository,
            IUserRepository userRepository
        )
            : base(notificationService, repository)
        {
            _transactionItemManager = transactionItemManager;
            _userRepository = userRepository;
        }

        public async Task Store(TransactionStoreDto dto)
        {
            if (ValidateDto(dto) is false)
                return;

            var transaction = await SetTransaction(dto);

            if (ValidateEntity(transaction) is false || transaction is null)
                return;

            if (ValidateTransationItemsRecord(dto, transaction))
                await _transactionItemManager.Manage(dto, transaction);

            _repository.Save(transaction);
        }

        private async Task<Transaction?> SetTransaction(TransactionStoreDto dto)
        {
            if (dto.IsRecorded)
                return await _repository.Find(dto.Id);

            return await CreateTransation(dto);
        }

        private async Task<Transaction?> CreateTransation(TransactionStoreDto dto)
        {
            var user = await GetUser(dto.UserId);

            if (HasNotifications)
                return default;

            return new Transaction()
            {
                User = user,
                Type = dto.Type,
                Date = dto.Date,
                Value = dto.Value
            };
        }

        private async Task<Transaction?> UpdateTransaction(TransactionStoreDto dto)
        {
            var transaction = await _repository.Find(dto.Id);

            if (ValidateNullableObject(transaction) is false || transaction is null)
                return default;

            if (dto.UserId != transaction.UserId)
            {
                var user = await GetUser(dto.UserId);

                if (HasNotifications)
                    return default;

                transaction.User = user;
            }

            if (dto.Type != transaction.Type)
                transaction.Type = dto.Type;

            if (dto.Date != transaction.Date)
                transaction.Date = dto.Date;

            if (dto.Value != transaction.Value)
                transaction.Value = dto.Value;

            return transaction;
        }

        private async Task<User?> GetUser(int userId)
        {
            var user = await _userRepository.Find(userId);

            if (ValidateNullableObject(user) is false)
                return default;

            return user;
        }

        private bool ValidateTransationItemsRecord(TransactionStoreDto dto, Transaction transaction)
        {
            var installmentsRequiredOnInsert = dto.IsRecorded is false && dto.InstallmentsIds.Any() is false;
            var installmentsRequiredOnUpdate = dto.IsRecorded &&
                (transaction.Value != dto.Value || transaction.Type != dto.Type) &&
                dto.InstallmentsIds.Any() is false;

            if (installmentsRequiredOnInsert || installmentsRequiredOnUpdate)
            {
                AddNotification("The balance installments is required to procced with the transaction record.");

                return false;
            }

            return true;
        }
    }
}
