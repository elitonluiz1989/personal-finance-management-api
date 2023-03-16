namespace PersonalFinanceManagement.Domain.Base.Filters
{
    public class Filter
    {
        public int Page { get; set; }
        public int PageSize { get; set; } = 10;
        public bool WithoutPagination { get; set; }
    }
}
