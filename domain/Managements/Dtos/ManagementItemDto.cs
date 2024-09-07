using PersonalFinanceManagement.Domain.Managements.Enums;

namespace PersonalFinanceManagement.Domain.Managements.Dtos
{
    public record ManagementItemDto : ManagementItemAmountDto
    {
        public int Id { get; set; }
        public int Reference { get; set; }
        public ManagementItemTypeEnum ManagementType { get; set; }
        public string Date { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
