using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Entites;

namespace PersonalFinanceManagement.Domain.Base.Specifications
{
    public abstract class Specification<TEntity, TKey, TFilters, TResult> : ISpecification<TEntity, TKey, TFilters, TResult>
        where TEntity : Entity<TKey>
        where TKey : struct
        where TFilters : class
        where TResult : class
    {
        protected IQueryable<TEntity> _query;
        protected readonly IRepository<TEntity, TKey> _repository;

        public Specification(IRepository<TEntity, TKey> repository)
        {
            _repository = repository;
            _query = _repository.Query();
        }

        public abstract ISpecification<TEntity, TKey, TFilters, TResult> WithFilter(TFilters filter, int authenticatedUserId, bool isAdmin);

        public abstract Task<IEnumerable<TResult>> List();

        public abstract Task<TResult?> First();
    }
}
