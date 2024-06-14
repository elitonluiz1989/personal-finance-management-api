using PersonalFinanceManagement.Domain.Base.Enums;

namespace PersonalFinanceManagement.Domain.Managements.Dtos
{
    public record ManagementItemAmountDto
    {
        public CommonTypeEnum Type { get; set; }
        public decimal Amount { get; set; }
    }
}
