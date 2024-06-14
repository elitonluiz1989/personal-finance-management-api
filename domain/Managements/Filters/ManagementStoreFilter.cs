namespace PersonalFinanceManagement.Domain.Managements.Filters
{
    public record ManagementStoreFilter
    {
        public int Reference { get; set; }
        public int? UserId { get; set; }
    }
}
