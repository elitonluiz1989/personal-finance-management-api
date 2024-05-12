using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Dtos;
using PersonalFinanceManagement.Domain.Base.Entites;

namespace PersonalFinanceManagement.Domain.Base.Services
{
    public abstract class Store<TEntity, TKey> : NotifiableService
        where TEntity : Entity<TKey>
        where TKey : struct
    {
        protected readonly IRepository<TEntity, TKey> _repository;

        public Store(
            INotificationService notificationService,
            IRepository<TEntity, TKey> repository
        )
            : base(notificationService)
        {
            _repository = repository;
        }

        protected bool ValidateDto(RecordedDto<TKey> dto)
        {
            return ValidateNullableObject(dto);
        }

        protected bool ValidateEntity(TEntity? entity)
        {
            if (HasNotifications)
                return false;

            if (ValidateNullableObject(entity) is false)
                return false;

            if (entity!.Validate() is false)
            {
                NotificationService.AddNotification(entity.Errors);

                return false;
            }

            return true;
        }

        protected TEntity? SaveEntity(TEntity? entity)
        {
            if (entity is null ||
                HasNotifications is true)
                return default;

            return _repository.Save(entity);
        }
    }
}