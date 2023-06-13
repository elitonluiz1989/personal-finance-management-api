namespace PersonalFinanceManagement.Domain.Base.Dtos
{
    public record PagedResultsDto<T>
    {
        public IEnumerable<T> Results { get; set; } = Enumerable.Empty<T>();
        public PaginationDto Pagination { get; set; } = new();
    }
}
