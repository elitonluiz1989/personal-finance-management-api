using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Dtos;
using PersonalFinanceManagement.Domain.Base.Entites;

namespace PersonalFinanceManagement.Domain.Base.Services
{
    public abstract class BaseStore<TEntity, TKey> : Store<TEntity, TKey>, IBaseStore<TKey>
        where TEntity : Entity<TKey>
        where TKey : struct
    {

        public BaseStore(
            INotificationService notificationService,
            IRepository<TEntity, TKey> repository
        )
            : base(notificationService, repository)
        {
        }

        public async Task Store(RecordedDto<TKey> dto)
        {
            if (ValidateDto(dto))
                return;

            var entity = await SetEntity(dto);

            if (ValidateEntity(entity))
                return;

            SaveEntity(entity);
        }

        protected abstract Task<TEntity?> SetEntity<TDto>(TDto dto)
            where TDto : RecordedDto<TKey>;
    }
}
