using PersonalFinanceManagement.Domain.Base.Entites;

namespace PersonalFinanceManagement.Domain.Base.Contracts
{
    public interface IRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
        where TKey : struct
    {
        IDBContext Context { get; }
        void Insert(TEntity entity);
        void Update(TEntity entity);
        Task Delete(TKey id);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        IQueryable<TEntity> Query();
        Task<IEnumerable<TEntity>> All();
        Task<TEntity?> Find(TKey id);
        TEntity? Save(TEntity entity);
    }
}
