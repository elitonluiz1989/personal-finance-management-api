using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Dtos;
using PersonalFinanceManagement.Domain.Base.Entites;
using PersonalFinanceManagement.Domain.Base.Filters;

namespace PersonalFinanceManagement.Domain.Base.Specifications
{
    public abstract class Specification<TEntity, TKey, TFilter, TResult> : ISpecification<TEntity, TKey, TFilter, TResult>
        where TEntity : Entity<TKey>
        where TKey : struct
        where TFilter : notnull, Filter
        where TResult : class
    {
        protected IQueryable<TEntity> Query;
        protected IQueryable<TEntity> QueryWithoutPagination;
        protected TFilter? Filter;
        protected readonly IRepository<TEntity, TKey> Repository;

        public Specification(IRepository<TEntity, TKey> repository)
        {
            Repository = repository;
            Query = Repository.Query();
        }

        public abstract ISpecification<TEntity, TKey, TFilter, TResult> WithFilter(TFilter filter, int authenticatedUserId, bool isAdmin);
        
        public virtual ISpecification<TEntity, TKey, TFilter, TResult> WithPagination(TFilter filter)
        {
            if (filter is null)
                return this;

            if (filter.WithoutPagination)
                return this;

            var page = filter.Page - 1;

            if (page < 0)
            {
                page = 0;

                if (Filter is not null)
                    Filter.Page = page;
            }

            var amountSkipped = page * filter.PageSize;

            QueryWithoutPagination = Query;
            Query = Query.Skip(amountSkipped).Take(filter.PageSize);

            return this;
        }

        public virtual async Task<IEnumerable<TResult>> List()
        {
            return await GetQuery().ToListAsync();
        }

        public virtual async Task<PagedResultsDto<TResult>> PagedList()
        {
            return await GetPagedResults();
        }

        public virtual async Task<TResult?> First()
        {
            return await GetQuery().FirstAsync();
        }


        protected async Task<PagedResultsDto<TResult>> GetPagedResults()
        {
            var query = GetQuery();

            return await GetPagedResults(query);
        }

        protected async Task<PagedResultsDto<TResult>> GetPagedResults(IQueryable<TResult> query)
        {
            var filter = Filter ?? new Filter();

            var totalResults = await QueryWithoutPagination.CountAsync();
            decimal totalPages = totalResults / filter.PageSize;

            var dto = new PagedResultsDto<TResult>
            {
                Results = await query.ToListAsync()
            };
            dto.Pagination.Page = filter.Page;
            dto.Pagination.TotalPages = (int)Math.Floor(totalPages);
            dto.Pagination.Total = totalResults;

            return dto;
        }

        protected abstract IQueryable<TResult> GetQuery();
    }
}
