using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Entites;

namespace PersonalFinanceManagement.Infra.Data.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
        where TKey : struct
    {
        protected readonly IDBContext Context;

        public Repository(IDBContext dbContext)
        {
            Context = dbContext;
        }

        public void Insert(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            DetachLocalEntity(entity);

            Context.Entry(entity).State = EntityState.Modified;
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
            if (VerifyIfIsSoftDelete(entity))
            {
                SetAsDeleted(entity);

                return;
            }

            DetachLocalEntity(entity);

            Context.Set<TEntity>().Remove(entity);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            if (VerifyIfIsSoftDelete(entities.First()))
            {
                foreach (var entity in entities)
                {
                    SetAsDeleted(entity);
                }

                return;
            }

            Context.Set<TEntity>().RemoveRange(entities);
        }

        public IQueryable<TEntity> Query()
        {
            return Context.Set<TEntity>();
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
                return;

            Insert(entity);
        }

        private static bool VerifyIfIsSoftDelete(TEntity entity)
        {
            return entity.IsRecorded && entity.WithSoftDelete;
        }

        private void SetAsDeleted(TEntity entity)
        {
            ((IEntityWithSoftDelete)entity).SetAsDeleted();
        }

        private void DetachLocalEntity(TEntity entity)
        {
            var localContextEntity = Context.Set<TEntity>().Local.FirstOrDefault(e => e.Id.Equals(entity.Id));

            if (localContextEntity is not null)
                Context.Entry(localContextEntity).State = EntityState.Detached;
        }
    }
}
