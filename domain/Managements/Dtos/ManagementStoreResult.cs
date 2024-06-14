using PersonalFinanceManagement.Domain.Base.Enums;
using PersonalFinanceManagement.Domain.Managements.Enums;

namespace PersonalFinanceManagement.Domain.Managements.Dtos
{
    public record ManagementStoreResult
    {
        public int Id { get; set; }
        public CommonTypeEnum Type { get; set; }
        public ManagementItemTypeEnum ManagementType { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
    }
}
