namespace PersonalFinanceManagement.Domain.Base.Filters
{
    public record Filter
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool WithoutPagination { get; set; }
    }
}
