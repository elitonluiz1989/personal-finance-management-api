using PersonalFinanceManagement.Domain.Base.Entites;

namespace PersonalFinanceManagement.Domain.Base.Contracts
{
    public interface ISpecification<TEntity, TKey, TFilters, TResult>
        where TEntity : Entity<TKey>
        where TKey : struct
        where TFilters : class
        where TResult : class
    {
        ISpecification<TEntity, TKey, TFilters, TResult> WithFilter(TFilters filter, int authenticatedUserId, bool isAdmin);
        Task<IEnumerable<TResult>> List();
        Task<TResult?> First();
    }
}
