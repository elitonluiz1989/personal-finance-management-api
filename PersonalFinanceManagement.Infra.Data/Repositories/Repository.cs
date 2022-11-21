using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Entites;

namespace PersonalFinanceManagement.Infra.Data.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
        where TKey : struct
    {
        protected readonly IDBContext DBContenxt;

        public Repository(IDBContext dbContext)
        {
            DBContenxt = dbContext;
        }

        public void Insert(TEntity entity)
        {
            DBContenxt.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            DetachLocalEntity(entity);

            DBContenxt.Entry(entity).State = EntityState.Modified;
        }

        public async Task Delete(TKey id)
        {
            var entity = await Find(id);

            if (entity is null)
                return;

            Delete(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity.IsRecorded && entity.WithSoftDelete)
            {
                ((IEntityWithSoftDelete)entity).SetAsDeleted();

                Update(entity);

                return;
            }

            DetachLocalEntity(entity);

            DBContenxt.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> Query()
        {
            return DBContenxt.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> All()
        {
            return await Query().ToListAsync();
        }

        public async Task<TEntity?> Find(TKey id)
        {
            var query = Query();
            
            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public void Save(TEntity entity)
        {
            if (entity.WithRegistrationDates)
                ((IEntityWithRegistrationDates)entity).SetRegistrationDates();

            if (entity.IsRecorded)
            {
                Update(entity);

                return;
            }

            Insert(entity);
        }

        private void DetachLocalEntity(TEntity entity)
        {
            var localContextEntity = DBContenxt.Set<TEntity>().Local.FirstOrDefault(e => e.Id.Equals(entity.Id));

            if (localContextEntity is not null)
                DBContenxt.Entry(localContextEntity).State = EntityState.Detached;
        }
    }
}
