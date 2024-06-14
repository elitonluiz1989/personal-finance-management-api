using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Entites;

namespace PersonalFinanceManagement.Infra.Data.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
        where TKey : struct
    {
        public IDBContext Context => _context;
        protected readonly IDBContext _context;

        public Repository(IDBContext dbContext)
        {
            _context = dbContext;
        }

        public void Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            DetachLocalEntity(entity);

            _context.Entry(entity).State = EntityState.Modified;
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
                Repository<TEntity, TKey>.SetAsDeleted(entity);

                return;
            }

            DetachLocalEntity(entity);

            _context.Set<TEntity>().Remove(entity);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            if (VerifyIfIsSoftDelete(entities.First()))
            {
                foreach (var entity in entities)
                {
                    Repository<TEntity, TKey>.SetAsDeleted(entity);
                }

                return;
            }

            _context.Set<TEntity>().RemoveRange(entities);
        }

        public IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>();
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

        public TEntity? Save(TEntity entity)
        {
            if (entity.WithRegistrationDates)
                ((IEntityWithRegistrationDates)entity).SetRegistrationDates();

            if (entity.IsRecorded)
                return default;

            Insert(entity);

            return entity;
        }

        private static bool VerifyIfIsSoftDelete(TEntity entity)
        {
            return entity.IsRecorded && entity.WithSoftDelete;
        }

        private static void SetAsDeleted(TEntity entity)
        {
            ((IEntityWithSoftDelete)entity).SetAsDeleted();
        }

        private void DetachLocalEntity(TEntity entity)
        {
            var localContextEntity = _context.Set<TEntity>().Local.FirstOrDefault(e => e.Id.Equals(entity.Id));

            if (localContextEntity is not null)
                _context.Entry(localContextEntity).State = EntityState.Detached;
        }
    }
}
