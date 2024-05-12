using PersonalFinanceManagement.Domain.Balances.Enums;
using PersonalFinanceManagement.Domain.Base.Enums;
using PersonalFinanceManagement.Domain.Managements.Enums;

namespace PersonalFinanceManagement.Domain.Managements.Dtos
{
    public record ManagementResult
    {
        public int Id { get; set; }
        public int Reference { get; set; }
        public CommonTypeEnum Type { get; set; }
        public InstallmentStatusEnum Status { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public ManagementItemTypeEnum ManagementType { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
