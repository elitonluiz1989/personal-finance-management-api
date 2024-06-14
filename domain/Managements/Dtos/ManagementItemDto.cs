namespace PersonalFinanceManagement.Domain.Managements.Dtos
{
    public record ManagementItemDto : ManagementItemAmountDto
    {
        public int Reference { get; set; }
        public string Date { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int RecordId { get; set; }
    }
}
