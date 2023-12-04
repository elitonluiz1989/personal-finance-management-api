using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Entites;

namespace PersonalFinanceManagement.Domain.Base.Services
{
    public abstract class BaseDeleter<TEntity, TKey, TRepository> : IBaseDeleter<TEntity, TKey>
        where TEntity : Entity<TKey>
        where TKey : struct
        where TRepository : IRepository<TEntity, TKey>
    {
        protected readonly INotificationService _notificationService;
        protected readonly TRepository _repository;

        public BaseDeleter(
            INotificationService notificationService,
            TRepository repository
        )
        {
            _notificationService = notificationService;
            _repository = repository;
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);

                if (_notificationService.HasNotifications())
                    break;
            }
        }

        public virtual async Task Delete(TKey id)
        {
            var entity = await Find(id);

            Delete(entity);
        }

        public virtual void Delete(TEntity? entity)
        {
            Validate(entity);

            if (entity is null || _notificationService.HasNotifications())
                return;

            _repository.Delete(entity);
        }

        protected virtual async Task<TEntity?> Find(TKey id)
        {
            return await _repository.Find(id);
        }

        protected virtual void Validate(TEntity? entity)
        {
            if (entity is not null)
                return;
            
            _notificationService.AddNotification($"{nameof(TEntity)} is null");
        }
    }
}
