using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Dtos;
using PersonalFinanceManagement.Domain.Base.Entites;

namespace PersonalFinanceManagement.Domain.Base.Services
{
    public abstract class Store<TEntity, TKey>
        where TEntity : Entity<TKey>
        where TKey : struct
    {
        protected readonly INotificationService _notificationService;
        protected readonly IRepository<TEntity, TKey> _repository;

        public Store(
            INotificationService notificationService,
            IRepository<TEntity, TKey> repository
        )
        {
            _notificationService = notificationService;
            _repository = repository;
        }

        protected bool ValidateDto(RecordedDto<TKey> dto)
        {
            if (dto is not null)
                return true;

            _notificationService.AddNotification($"{nameof(dto)} is null");

            return false;
        }

        protected bool ValidateEntity(TEntity? entity)
        {
            if (entity is null || _notificationService.HasNotifications())
                return false;

            if (entity.Validate() is false)
            {
                _notificationService.AddNotification(entity.Errors);

                return false;
            }

            return true;
        }

        protected void SaveEntity(TEntity? entity)
        {
            if (entity is null ||
                _notificationService.HasNotifications() is true)
                return;

            if (entity.WithRegistrationDates)
                ((IEntityWithRegistrationDates)entity).SetRegistrationDates();

            if (entity.IsRecorded is true )
                return;

            _repository.Save(entity);
        }
    }
}