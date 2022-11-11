using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Dtos;
using PersonalFinanceManagement.Domain.Base.Entites;

namespace PersonalFinanceManagement.Domain.Base.Services
{
    public abstract class BaseStore<TEntity, TKey> : IBaseStore<TKey>
        where TEntity : Entity<TKey>
        where TKey : struct
    {
        protected readonly INotificationService _notificationService;
        protected readonly IRepository<TEntity, TKey> _repository;

        public BaseStore(
            INotificationService notificationService,
            IRepository<TEntity, TKey> repository
        )
        {
            _notificationService = notificationService;
            _repository = repository;
        }

        public async Task Store(RecordedDto<TKey> dto)
        {
            if (dto is null)
            {
                _notificationService.AddNotification($"{nameof(dto)} is null");

                return;
            }

            var entity = await SetEntity(dto);

            if (entity is null || _notificationService.HasNotifications())
                return;

            if (entity.Validate() is false)
                _notificationService.AddNotification(entity.Errors);

            if (_notificationService.HasNotifications() is false &&
                entity.IsRecorded is false)
                _repository.Save(entity);
        }

        protected abstract Task<TEntity?> SetEntity<TDto>(TDto dto)
            where TDto : RecordedDto<TKey>;
    }
}
