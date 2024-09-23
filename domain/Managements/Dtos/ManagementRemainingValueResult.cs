using PersonalFinanceManagement.Domain.Base.Enums;
using PersonalFinanceManagement.Domain.Managements.Enums;

namespace PersonalFinanceManagement.Domain.Managements.Dtos
{
    public record ManagementRemainingValueResult
    {
        public CommonTypeEnum Type { get; set; }
        public ManagementItemTypeEnum ManagementType { get; set; }
        public decimal Amount { get; set; }
        public decimal? AmountPaid { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
