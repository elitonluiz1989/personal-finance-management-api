using PersonalFinanceManagement.Domain.Base.Entites;

namespace PersonalFinanceManagement.Domain.Base.Contracts
{
    public interface IBaseDeleter<TEntity, TKey>
        where TEntity : Entity<TKey>
        where TKey : struct
    {
        void Delete(IEnumerable<TEntity> entities);
        Task Delete(TKey id);
        void Delete(TEntity? entity);
    }
}
