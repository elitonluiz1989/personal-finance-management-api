using PersonalFinanceManagement.Domain.Base.Enums;
using PersonalFinanceManagement.Domain.Managements.Enums;

namespace PersonalFinanceManagement.Domain.Managements.Dtos
{
    public record ManagementItemDto
    {
        public int Reference { get; set; }
        public CommonTypeEnum Type { get; set; }
        public ManagementItemTypeEnum ManagementType { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int RecordId { get; set; }
    }
}
