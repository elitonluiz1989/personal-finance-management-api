namespace PersonalFinanceManagement.Domain.Base.Dtos
{
    public record PaginationDto
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int Total { get; set; }
    }
}
