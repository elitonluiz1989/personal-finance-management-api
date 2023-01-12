using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Entites;

namespace PersonalFinanceManagement.Domain.Base.Services
{
    public abstract class BaseDeleter<TEntity, TKey> : IBaseDeleter<TKey>
        where TEntity : Entity<TKey>
        where TKey : struct
    {
        protected readonly INotificationService _notificationService;
        protected readonly IRepository<TEntity, TKey> _repository;

        public BaseDeleter(
            INotificationService notificationService,
            IRepository<TEntity, TKey> repository
        )
        {
            _notificationService = notificationService;
            _repository = repository;
        }

        public async Task Delete(TKey id)
        {
            var entity = await _repository.Find(id);

            Validate(entity);

            if (entity is null || _notificationService.HasNotifications())
                return;

            _repository.Delete(entity);
        }

        protected abstract void Validate(IEntity? entity);
    }
}
