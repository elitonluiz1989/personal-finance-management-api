using PersonalFinanceManagement.Domain.Base.Dtos;
using PersonalFinanceManagement.Domain.Base.Entites;
using PersonalFinanceManagement.Domain.Base.Filters;

namespace PersonalFinanceManagement.Domain.Base.Contracts
{
    public interface ISpecification<TEntity, TKey, TFilter, TResult>
        where TEntity : Entity<TKey>
        where TKey : struct
        where TFilter : notnull, Filter
        where TResult : class
    {
        ISpecification<TEntity, TKey, TFilter, TResult> WithFilter(TFilter filter, int authenticatedUserId, bool isAdmin);
        ISpecification<TEntity, TKey, TFilter, TResult> WithPagination(TFilter filter);
        Task<IEnumerable<TResult>> List();
        Task<PagedResultsDto<TResult>> PagedList();
        Task<TResult?> First();
    }
}
